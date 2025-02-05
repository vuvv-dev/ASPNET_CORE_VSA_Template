using System;
using System.Threading;
using System.Threading.Tasks;
using F11.Common;
using F11.DataAccess;
using F11.Models;
using FCommon.FeatureService;
using FCommon.IdGeneration;

namespace F11.BusinessLogic;

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
        var doesListExist = await _repository.Value.DoesTodoTaskListExistAsync(
            request.TodoTaskListId,
            ct
        );
        if (!doesListExist)
        {
            return Constant.DefaultResponse.App.TODO_TASK_LIST_NOT_FOUND;
        }

        var todoTask = new TaskTodoModel
        {
            Id = _idGenerator.Value.NextId(),
            Content = request.Content,
            CreatedDate = DateTime.UtcNow,
            TodoTaskListId = request.TodoTaskListId,
        };
        var isTodoTaskCreated = await _repository.Value.CreateTodoTaskAsync(todoTask, ct);
        if (!isTodoTaskCreated)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new()
        {
            AppCode = Constant.AppCode.SUCCESS,
            Body = new() { TodoTaskId = todoTask.Id },
        };
    }
}
