using System;
using System.Threading;
using System.Threading.Tasks;
using F1.Common;
using F1.DataAccess;
using F1.Models;
using FCommon.AccessToken;
using FCommon.Constants;
using FCommon.FeatureService;
using FCommon.IdGeneration;
using FCommon.RefreshToken;

namespace F1.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;
    private readonly Lazy<IAppRefreshTokenHandler> _refreshTokenHandler;
    private readonly Lazy<IAppAccessTokenHandler> _accessTokenHandler;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public Service(
        Lazy<IRepository> repository,
        Lazy<IAppAccessTokenHandler> accessTokenHandler,
        Lazy<IAppRefreshTokenHandler> refreshTokenHandler,
        Lazy<IAppIdGenerator> idGenerator
    )
    {
        _repository = repository;
        _accessTokenHandler = accessTokenHandler;
        _refreshTokenHandler = refreshTokenHandler;
        _idGenerator = idGenerator;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var isUserFound = await _repository.Value.IsUserFoundByEmailAsync(request.Email, ct);
        if (!isUserFound)
        {
            return Constant.DefaultResponse.App.USER_NOT_FOUND;
        }

        var passwordSignInResult = await _repository.Value.CheckPasswordSignInAsync(
            request.Email,
            request.Password,
            ct
        );
        if (!passwordSignInResult.IsSuccessful)
        {
            if (passwordSignInResult.IsLockedOut)
            {
                return Constant.DefaultResponse.App.TEMPORARY_BANNED;
            }
            return Constant.DefaultResponse.App.PASSWORD_IS_INCORRECT;
        }

        var tokenId = _idGenerator.Value.NextId().ToString();
        var newRefreshToken = InitNewRefreshToken(
            passwordSignInResult.UserId,
            tokenId,
            request.RememberMe
        );
        var result = await _repository.Value.CreateRefreshTokenAsync(newRefreshToken, ct);
        if (!result)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        var newAccessToken = _accessTokenHandler.Value.GenerateJWT(
            [
                new(AppConstant.JsonWebToken.ClaimType.JTI, tokenId.ToString()),
                new(AppConstant.JsonWebToken.ClaimType.SUB, passwordSignInResult.UserId.ToString()),
                new(
                    AppConstant.JsonWebToken.ClaimType.PURPOSE.Name,
                    AppConstant.JsonWebToken.ClaimType.PURPOSE.Value.USER_IN_APP
                ),
            ],
            Constant.APP_USER_ACCESS_TOKEN.DURATION_IN_MINUTES
        );

        return new()
        {
            AppCode = Constant.AppCode.SUCCESS,
            Body = new() { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Value },
        };
    }

    private RefreshTokenModel InitNewRefreshToken(long userId, string tokenId, bool isRememberMe)
    {
        return new()
        {
            LoginProvider = tokenId,
            ExpiredAt = isRememberMe
                ? DateTime.UtcNow.AddDays(
                    Constant.APP_USER_REFRESH_TOKEN.DURATION_IN_MINUTES.REMEMBER_ME
                )
                : DateTime.UtcNow.AddDays(
                    Constant.APP_USER_REFRESH_TOKEN.DURATION_IN_MINUTES.NOT_REMEMBER_ME
                ),
            UserId = userId,
            Value = _refreshTokenHandler.Value.Generate(),
            Name = Constant.APP_USER_REFRESH_TOKEN.NAME,
        };
    }
}
