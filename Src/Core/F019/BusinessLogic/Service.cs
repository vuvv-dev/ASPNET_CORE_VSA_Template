using System;
using System.Threading;
using System.Threading.Tasks;
using F019.Common;
using F019.DataAccess;
using F019.Models;
using FCommon.FeatureService;

namespace F019.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;

    public Service(Lazy<IRepository> repository)
    {
        _repository = repository;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var doesTaskExist = await _repository.Value.DoesTodoTaskExistAsync(request.TodoTaskId, ct);
        if (!doesTaskExist)
        {
            return Constant.DefaultResponse.App.TASK_NOT_FOUND;
        }

        var isSuccess = await _repository.Value.ChangeContentAsync(
            request.TodoTaskId,
            request.Content,
            ct
        );
        if (!isSuccess)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new() { AppCode = Constant.AppCode.SUCCESS };
    }
}
