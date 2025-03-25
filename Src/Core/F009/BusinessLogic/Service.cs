using System;
using System.Threading;
using System.Threading.Tasks;
using F009.Common;
using F009.DataAccess;
using F009.Models;
using FCommon.FeatureService;

namespace F009.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;

    public Service(Lazy<IRepository> repository)
    {
        _repository = repository;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var doesListExist = await _repository.Value.DoesTaskTodoListExistAsync(
            request.TodoTaskListId,
            ct
        );
        if (!doesListExist)
        {
            return Constant.DefaultResponse.App.LIST_DOES_NOT_EXIST;
        }

        var todoTaskListModel = new TaskTodoListModel
        {
            Id = request.TodoTaskListId,
            Name = request.NewName,
        };

        var isRemoved = await _repository.Value.UpdateTaskTodoListAsync(todoTaskListModel, ct);
        if (!isRemoved)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new() { AppCode = Constant.AppCode.SUCCESS };
    }
}
