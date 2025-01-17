using System;
using System.Threading;
using System.Threading.Tasks;
using F6.Src.Common;
using F6.Src.DataAccess;
using F6.Src.Models;
using FCommon.Src.AccessToken;
using FCommon.Src.Constants;
using FCommon.Src.FeatureService;
using FCommon.Src.IdGeneration;
using FCommon.Src.RefreshToken;

namespace F6.Src.BusinessLogic;

public sealed class F6Service : IServiceHandler<F6AppRequestModel, F6AppResponseModel>
{
    private readonly Lazy<IF6Repository> _repository;
    private readonly Lazy<IAppRefreshTokenHandler> _refreshTokenHandler;
    private readonly Lazy<IAppAccessTokenHandler> _accessTokenHandler;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public F6Service(
        Lazy<IF6Repository> repository,
        Lazy<IAppRefreshTokenHandler> refreshTokenHandler,
        Lazy<IAppAccessTokenHandler> accessTokenHandler,
        Lazy<IAppIdGenerator> idGenerator
    )
    {
        _repository = repository;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
        _idGenerator = idGenerator;
    }

    public async Task<F6AppResponseModel> ExecuteAsync(
        F6AppRequestModel request,
        CancellationToken ct
    )
    {
        var refreshTokenIdAsTring = request.AccessTokenId.ToString();

        var foundToken = await _repository.Value.DoesRefreshTokenBelongToAccessTokenAsync(
            refreshTokenIdAsTring,
            request.RefreshToken,
            ct
        );
        if (Equals(foundToken, null))
        {
            return F6Constant.DefaultResponse.App.REFRESH_TOKEN_DOES_NOT_EXIST;
        }
        if (foundToken.ExpiredAt < DateTime.UtcNow)
        {
            return F6Constant.DefaultResponse.App.REFRESH_TOKEN_EXPIRED;
        }

        var newRefreshToken = new F6UpdateRefreshTokenModel
        {
            CurrentId = refreshTokenIdAsTring,
            NewId = _idGenerator.Value.NextId().ToString(),
        };

        var result = await _repository.Value.UpdateRefreshTokenAsync(newRefreshToken, ct);
        if (!result)
        {
            return F6Constant.DefaultResponse.App.SERVER_ERROR;
        }

        var newAccessToken = _accessTokenHandler.Value.GenerateJWS(
            [
                new(AppConstants.JsonWebToken.ClaimType.JTI, newRefreshToken.NewId),
                new(AppConstants.JsonWebToken.ClaimType.SUB, request.UserId.ToString()),
                new(
                    AppConstants.JsonWebToken.ClaimType.PURPOSE.Name,
                    AppConstants.JsonWebToken.ClaimType.PURPOSE.Value.USER_IN_APP
                ),
            ],
            F6Constant.APP_USER_ACCESS_TOKEN.DURATION_IN_MINUTES
        );

        return new()
        {
            AppCode = F6Constant.AppCode.SUCCESS,
            Body = new() { AccessToken = newAccessToken, RefreshToken = request.RefreshToken },
        };
    }
}
