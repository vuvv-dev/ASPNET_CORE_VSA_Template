using System;
using System.Threading;
using System.Threading.Tasks;
using F003.Common;
using F003.DataAccess;
using F003.Models;
using FCommon.FeatureService;
using FCommon.IdGeneration;

namespace F003.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public Service(Lazy<IRepository> repository, Lazy<IAppIdGenerator> idGenerator)
    {
        _repository = repository;
        _idGenerator = idGenerator;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var isEmailFound = await _repository.Value.DoesEmailExistsAsync(request.Email, ct);
        if (isEmailFound)
        {
            return Constant.DefaultResponse.App.EMAIL_ALREADY_EXISTS;
        }

        var isPasswordValid = await _repository.Value.IsPasswordValidAsync(
            request.Email,
            request.Password,
            ct
        );
        if (!isPasswordValid)
        {
            return Constant.DefaultResponse.App.PASSWORD_IS_INVALID;
        }

        var user = CreateNewUser(request);
        var result = await _repository.Value.CreateUserAsync(user, ct);
        if (!result)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new() { AppCode = Constant.AppCode.SUCCESS };
    }

    private UserInfoModel CreateNewUser(AppRequestModel appRequest)
    {
        return new()
        {
            Id = _idGenerator.Value.NextId(),
            Email = appRequest.Email,
            Password = appRequest.Password,
            EmailConfirmed = true,
            AdditionalUserInfo = new()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                Description = string.Empty,
            },
        };
    }
}
