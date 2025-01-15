using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using F4.Src.Common;
using F4.Src.DataAccess;
using F4.Src.Models;
using FCommon.Src.FeatureService;
using FCommon.Src.IdGeneration;
using Microsoft.AspNetCore.WebUtilities;

namespace F4.Src.BusinessLogic;

public sealed class F4Service : IServiceHandler<F4AppRequestModel, F4AppResponseModel>
{
    private readonly Lazy<IF4Repository> _repository;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public F4Service(Lazy<IF4Repository> repository, Lazy<IAppIdGenerator> idGenerator)
    {
        _repository = repository;
        _idGenerator = idGenerator;
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

        var tokenAsBytes = Encoding.UTF8.GetBytes(resetPasswordToken.Value);
        var tokenAsBase64 = WebEncoders.Base64UrlEncode(tokenAsBytes);

        return new()
        {
            AppCode = F4Constant.AppCode.SUCCESS,
            Body = new() { Token = tokenAsBase64 },
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
            ExpiredAt = DateTime.UtcNow.AddMinutes(15),
            UserId = userId,
            Value = value,
            Name = F4Constant.APP_USER_PASSWORD_RESET_TOKEN_NAME,
        };
    }
}
