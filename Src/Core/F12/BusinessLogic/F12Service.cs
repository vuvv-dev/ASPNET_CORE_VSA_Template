using System;
using System.Threading;
using System.Threading.Tasks;
using F12.Common;
using F12.DataAccess;
using F12.Models;
using FCommon.FeatureService;

namespace F12.BusinessLogic;

public sealed class F12Service : IServiceHandler<F12AppRequestModel, F12AppResponseModel>
{
    private readonly Lazy<IF12Repository> _repository;

    public F12Service(Lazy<IF12Repository> repository)
    {
        _repository = repository;
    }

    public async Task<F12AppResponseModel> ExecuteAsync(
        F12AppRequestModel request,
        CancellationToken ct
    )
    {
        var doesTaskExist = await _repository.Value.DoesTodoTaskExistAsync(request.TodoTaskId, ct);
        if (!doesTaskExist)
        {
            return F12Constant.DefaultResponse.App.TODO_TASK_NOT_FOUND;
        }

        var isRemoved = await _repository.Value.RemoveTodoTaskAsync(request.TodoTaskId, ct);
        if (!isRemoved)
        {
            return F12Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new() { AppCode = F12Constant.AppCode.SUCCESS };
    }
}
