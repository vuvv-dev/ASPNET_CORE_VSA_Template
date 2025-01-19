using System;
using System.Threading;
using System.Threading.Tasks;
using F4.Common;
using F4.DataAccess;
using F4.Models;
using FCommon.AccessToken;
using FCommon.Constants;
using FCommon.FeatureService;
using FCommon.IdGeneration;

namespace F4.BusinessLogic;

public sealed class F4Service : IServiceHandler<F4AppRequestModel, F4AppResponseModel>
{
    private readonly Lazy<IF4Repository> _repository;
    private readonly Lazy<IAppIdGenerator> _idGenerator;
    private readonly Lazy<IAppAccessTokenHandler> _accessTokenHandler;

    public F4Service(
        Lazy<IF4Repository> repository,
        Lazy<IAppIdGenerator> idGenerator,
        Lazy<IAppAccessTokenHandler> accessTokenHandler
    )
    {
        _repository = repository;
        _idGenerator = idGenerator;
        _accessTokenHandler = accessTokenHandler;
    }

    public async Task<F4AppResponseModel> ExecuteAsync(
        F4AppRequestModel request,
        CancellationToken ct
    )
    {
        var userId = await _repository.Value.GetUserIdAsync(request.Email, ct);
        if (userId == 0)
        {
            return F4Constant.DefaultResponse.App.USER_NOT_FOUND;
        }

        var resetPasswordToken = await InitNewResetPasswordToken(userId, ct);
        var isTokenCreated = await _repository.Value.CreateResetPasswordTokenAsync(
            resetPasswordToken,
            ct
        );
        if (!isTokenCreated)
        {
            return F4Constant.DefaultResponse.App.SERVER_ERROR;
        }

        var newAccessToken = _accessTokenHandler.Value.GenerateJWS(
            [
                new(AppConstants.JsonWebToken.ClaimType.JTI, resetPasswordToken.LoginProvider),
                new(AppConstants.JsonWebToken.ClaimType.SUB, userId.ToString()),
                new(
                    AppConstants.JsonWebToken.ClaimType.PURPOSE.Name,
                    AppConstants.JsonWebToken.ClaimType.PURPOSE.Value.USER_PASSWORD_RESET
                ),
            ],
            F4Constant.APP_USER_PASSWORD_RESET_TOKEN.DURATION_IN_MINUTES
        );

        return new()
        {
            AppCode = F4Constant.AppCode.SUCCESS,
            Body = new() { ResetPasswordToken = newAccessToken },
        };
    }

    private async Task<F4ResetPasswordTokenModel> InitNewResetPasswordToken(
        long userId,
        CancellationToken ct
    )
    {
        var id = _idGenerator.Value.NextId();
        var value = await _repository.Value.GeneratePasswordResetTokenAsync(userId, ct);

        return new()
        {
            LoginProvider = id.ToString(),
            ExpiredAt = DateTime.UtcNow.AddMinutes(
                F4Constant.APP_USER_PASSWORD_RESET_TOKEN.DURATION_IN_MINUTES
            ),
            UserId = userId,
            Value = value,
            Name = F4Constant.APP_USER_PASSWORD_RESET_TOKEN.NAME,
        };
    }
}
