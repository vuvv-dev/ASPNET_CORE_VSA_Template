using System;
using System.Threading;
using System.Threading.Tasks;
using F17.Common;
using F17.DataAccess;
using F17.Models;
using FCommon.FeatureService;

namespace F17.BusinessLogic;

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

        var isSuccess = await _repository.Value.ModifyCompletedStatusAsync(
            request.TodoTaskId,
            request.IsCompleted,
            ct
        );
        if (!isSuccess)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new() { AppCode = Constant.AppCode.SUCCESS };
    }
}
