using System;
using System.Threading;
using System.Threading.Tasks;
using F005.Common;
using F005.DataAccess;
using F005.Models;
using FCommon.FeatureService;

namespace F005.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;

    public Service(Lazy<IRepository> repository)
    {
        _repository = repository;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var tokenIdAsString = request.ResetPasswordTokenId.ToString();

        var tokenValue = await _repository.Value.GetResetPasswordTokenByIdAsync(
            tokenIdAsString,
            ct
        );
        if (Equals(tokenValue, null))
        {
            return Constant.DefaultResponse.App.TOKEN_DOES_NOT_EXIST;
        }

        var isPasswordValid = await _repository.Value.IsPasswordValidAsync(request.NewPassword, ct);
        if (!isPasswordValid)
        {
            return Constant.DefaultResponse.App.PASSWORD_IS_INVALID;
        }

        var isPasswordResetSuccessfully = await _repository.Value.ResetPasswordAsync(
            request.UserId,
            request.NewPassword,
            tokenValue,
            tokenIdAsString,
            ct
        );
        if (!isPasswordResetSuccessfully)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new() { AppCode = Constant.AppCode.SUCCESS };
    }
}
