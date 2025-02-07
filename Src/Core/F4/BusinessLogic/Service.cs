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

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;
    private readonly Lazy<IAppIdGenerator> _idGenerator;
    private readonly Lazy<IAppAccessTokenHandler> _accessTokenHandler;

    public Service(
        Lazy<IRepository> repository,
        Lazy<IAppIdGenerator> idGenerator,
        Lazy<IAppAccessTokenHandler> accessTokenHandler
    )
    {
        _repository = repository;
        _idGenerator = idGenerator;
        _accessTokenHandler = accessTokenHandler;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var userId = await _repository.Value.GetUserIdAsync(request.Email, ct);
        if (userId == 0)
        {
            return Constant.DefaultResponse.App.USER_NOT_FOUND;
        }

        var resetPasswordToken = await InitNewResetPasswordToken(userId, ct);
        var isTokenCreated = await _repository.Value.CreateResetPasswordTokenAsync(
            resetPasswordToken,
            ct
        );
        if (!isTokenCreated)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        var newAccessToken = _accessTokenHandler.Value.GenerateJWT(
            [
                new(AppConstant.JsonWebToken.ClaimType.JTI, resetPasswordToken.LoginProvider),
                new(AppConstant.JsonWebToken.ClaimType.SUB, userId.ToString()),
                new(
                    AppConstant.JsonWebToken.ClaimType.PURPOSE.Name,
                    AppConstant.JsonWebToken.ClaimType.PURPOSE.Value.USER_PASSWORD_RESET
                ),
            ],
            Constant.APP_USER_PASSWORD_RESET_TOKEN.DURATION_IN_MINUTES
        );

        return new()
        {
            AppCode = Constant.AppCode.SUCCESS,
            Body = new() { ResetPasswordToken = newAccessToken },
        };
    }

    private async Task<ResetPasswordTokenModel> InitNewResetPasswordToken(
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
                Constant.APP_USER_PASSWORD_RESET_TOKEN.DURATION_IN_MINUTES
            ),
            UserId = userId,
            Value = value,
            Name = Constant.APP_USER_PASSWORD_RESET_TOKEN.NAME,
        };
    }
}
