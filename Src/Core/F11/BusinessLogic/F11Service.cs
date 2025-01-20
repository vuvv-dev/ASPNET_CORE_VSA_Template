using System;
using System.Threading;
using System.Threading.Tasks;
using F11.Common;
using F11.DataAccess;
using F11.Models;
using FCommon.FeatureService;
using FCommon.IdGeneration;

namespace F11.BusinessLogic;

public sealed class F11Service : IServiceHandler<F11AppRequestModel, F11AppResponseModel>
{
    private readonly Lazy<IF11Repository> _repository;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public F11Service(Lazy<IF11Repository> repository, Lazy<IAppIdGenerator> idGenerator)
    {
        _repository = repository;
        _idGenerator = idGenerator;
    }

    public async Task<F11AppResponseModel> ExecuteAsync(
        F11AppRequestModel request,
        CancellationToken ct
    )
    {
        var doesListExist = await _repository.Value.DoesTodoTaskListExistAsync(
            request.TodoTaskListId,
            ct
        );
        if (!doesListExist)
        {
            return F11Constant.DefaultResponse.App.TODO_TASK_LIST_NOT_FOUND;
        }

        var todoTask = new F11TaskTodoModel
        {
            Id = _idGenerator.Value.NextId(),
            Content = request.Content,
            CreatedDate = DateTime.UtcNow,
            TodoTaskListId = request.TodoTaskListId,
        };
        var isTodoTaskCreated = await _repository.Value.CreateTodoTaskAsync(todoTask, ct);
        if (!isTodoTaskCreated)
        {
            return F11Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new()
        {
            AppCode = F11Constant.AppCode.SUCCESS,
            Body = new() { TodoTaskId = todoTask.Id },
        };
    }
}
