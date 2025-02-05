using System;
using System.Threading;
using System.Threading.Tasks;
using F12.Common;
using F12.DataAccess;
using F12.Models;
using FCommon.FeatureService;

namespace F12.BusinessLogic;

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
            return Constant.DefaultResponse.App.TODO_TASK_NOT_FOUND;
        }

        var isRemoved = await _repository.Value.RemoveTodoTaskAsync(request.TodoTaskId, ct);
        if (!isRemoved)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new() { AppCode = Constant.AppCode.SUCCESS };
    }
}
