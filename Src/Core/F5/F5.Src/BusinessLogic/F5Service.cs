using System;
using System.Threading;
using System.Threading.Tasks;
using F5.Src.Common;
using F5.Src.DataAccess;
using F5.Src.Models;
using FCommon.Src.FeatureService;

namespace F5.Src.BusinessLogic;

public sealed class F5Service : IServiceHandler<F5AppRequestModel, F5AppResponseModel>
{
    private readonly Lazy<IF5Repository> _repository;

    public F5Service(Lazy<IF5Repository> repository)
    {
        _repository = repository;
    }

    public async Task<F5AppResponseModel> ExecuteAsync(
        F5AppRequestModel request,
        CancellationToken ct
    )
    {
        var tokenIdAsString = request.ResetPasswordTokenId.ToString();

        var tokenValue = await _repository.Value.GetResetPasswordTokenByIdAsync(
            tokenIdAsString,
            ct
        );
        if (Equals(tokenValue, null))
        {
            return F5Constant.DefaultResponse.App.TOKEN_DOES_NOT_EXIST;
        }

        var isPasswordValid = await _repository.Value.IsPasswordValidAsync(request.NewPassword, ct);
        if (!isPasswordValid)
        {
            return F5Constant.DefaultResponse.App.PASSWORD_IS_INVALID;
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
            return F5Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new() { AppCode = F5Constant.AppCode.SUCCESS };
    }
}
