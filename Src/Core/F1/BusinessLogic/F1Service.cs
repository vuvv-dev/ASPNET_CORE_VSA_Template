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

public sealed class F1Service : IServiceHandler<F1AppRequestModel, F1AppResponseModel>
{
    private readonly Lazy<IF1Repository> _repository;
    private readonly Lazy<IAppRefreshTokenHandler> _refreshTokenHandler;
    private readonly Lazy<IAppAccessTokenHandler> _accessTokenHandler;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public F1Service(
        Lazy<IF1Repository> repository,
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

    public async Task<F1AppResponseModel> ExecuteAsync(
        F1AppRequestModel request,
        CancellationToken ct
    )
    {
        var isUserFound = await _repository.Value.IsUserFoundByEmailAsync(request.Email, ct);
        if (!isUserFound)
        {
            return F1Constant.DefaultResponse.App.USER_NOT_FOUND;
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
                return F1Constant.DefaultResponse.App.TEMPORARY_BANNED;
            }
            return F1Constant.DefaultResponse.App.PASSWORD_IS_INCORRECT;
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
            return F1Constant.DefaultResponse.App.SERVER_ERROR;
        }

        var newAccessToken = _accessTokenHandler.Value.GenerateJWS(
            [
                new(AppConstants.JsonWebToken.ClaimType.JTI, tokenId.ToString()),
                new(
                    AppConstants.JsonWebToken.ClaimType.SUB,
                    passwordSignInResult.UserId.ToString()
                ),
                new(
                    AppConstants.JsonWebToken.ClaimType.PURPOSE.Name,
                    AppConstants.JsonWebToken.ClaimType.PURPOSE.Value.USER_IN_APP
                ),
            ],
            F1Constant.APP_USER_ACCESS_TOKEN.DURATION_IN_MINUTES
        );

        return new()
        {
            AppCode = F1Constant.AppCode.SUCCESS,
            Body = new() { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Value },
        };
    }

    private F1RefreshTokenModel InitNewRefreshToken(long userId, string tokenId, bool isRememberMe)
    {
        return new()
        {
            LoginProvider = tokenId,
            ExpiredAt = isRememberMe
                ? DateTime.UtcNow.AddDays(
                    F1Constant.APP_USER_REFRESH_TOKEN.DURATION_IN_MINUTES.REMEMBER_ME
                )
                : DateTime.UtcNow.AddDays(
                    F1Constant.APP_USER_REFRESH_TOKEN.DURATION_IN_MINUTES.NOT_REMEMBER_ME
                ),
            UserId = userId,
            Value = _refreshTokenHandler.Value.Generate(),
            Name = F1Constant.APP_USER_REFRESH_TOKEN.NAME,
        };
    }
}
