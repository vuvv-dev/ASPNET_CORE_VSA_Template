using System;
using System.Threading;
using System.Threading.Tasks;
using F6.Common;
using F6.DataAccess;
using F6.Models;
using FCommon.AccessToken;
using FCommon.Constants;
using FCommon.FeatureService;
using FCommon.IdGeneration;

namespace F6.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;
    private readonly Lazy<IAppAccessTokenHandler> _accessTokenHandler;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public Service(
        Lazy<IRepository> repository,
        Lazy<IAppAccessTokenHandler> accessTokenHandler,
        Lazy<IAppIdGenerator> idGenerator
    )
    {
        _repository = repository;
        _accessTokenHandler = accessTokenHandler;
        _idGenerator = idGenerator;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var refreshTokenIdAsTring = request.AccessTokenId.ToString();

        var foundToken = await _repository.Value.DoesRefreshTokenBelongToAccessTokenAsync(
            refreshTokenIdAsTring,
            request.RefreshToken,
            ct
        );
        if (Equals(foundToken, null))
        {
            return Constant.DefaultResponse.App.REFRESH_TOKEN_DOES_NOT_EXIST;
        }
        if (foundToken.ExpiredAt < DateTime.UtcNow)
        {
            return Constant.DefaultResponse.App.REFRESH_TOKEN_EXPIRED;
        }

        var newRefreshToken = new UpdateRefreshTokenModel
        {
            CurrentId = refreshTokenIdAsTring,
            NewId = _idGenerator.Value.NextId().ToString(),
        };

        var result = await _repository.Value.UpdateRefreshTokenAsync(newRefreshToken, ct);
        if (!result)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        var newAccessToken = _accessTokenHandler.Value.GenerateJWT(
            [
                new(AppConstant.JsonWebToken.ClaimType.JTI, newRefreshToken.NewId),
                new(AppConstant.JsonWebToken.ClaimType.SUB, request.UserId.ToString()),
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
            Body = new() { AccessToken = newAccessToken, RefreshToken = request.RefreshToken },
        };
    }
}
