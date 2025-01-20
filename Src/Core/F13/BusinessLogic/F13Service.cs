using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F13.Common;
using F13.DataAccess;
using F13.Models;
using FCommon.FeatureService;

namespace F13.BusinessLogic;

public sealed class F13Service : IServiceHandler<F13AppRequestModel, F13AppResponseModel>
{
    private readonly Lazy<IF13Repository> _repository;

    public F13Service(Lazy<IF13Repository> repository)
    {
        _repository = repository;
    }

    public async Task<F13AppResponseModel> ExecuteAsync(
        F13AppRequestModel request,
        CancellationToken ct
    )
    {
        var doesListExist = await _repository.Value.DoesTodoTaskListExistAsync(
            request.TodoTaskListId,
            ct
        );
        if (!doesListExist)
        {
            return F13Constant.DefaultResponse.App.TODO_TASK_LIST_NOT_FOUND;
        }

        var doesTaskExist = await _repository.Value.DoesTodoTaskExistAsync(
            request.TodoTaskId,
            request.TodoTaskListId,
            ct
        );
        if (!doesTaskExist && request.TodoTaskId != 0)
        {
            return F13Constant.DefaultResponse.App.TASK_NOT_FOUND;
        }

        var input = new F13GetTodoTasksInputModel
        {
            TodoTaskId = request.TodoTaskId,
            TodoTaskListId = request.TodoTaskListId,
            NumberOfRecord = request.NumberOfRecord,
        };
        var foundTodoTasks = await _repository.Value.GetTodoTasksAsync(input, ct);

        var taskCount = foundTodoTasks.Count();
        if (taskCount <= request.NumberOfRecord)
        {
            return new()
            {
                AppCode = F13Constant.AppCode.SUCCESS,
                Body = new()
                {
                    TodoTasks = foundTodoTasks.Select(
                        model => new F13AppResponseModel.BodyModel.TodoTaskModel
                        {
                            Id = model.Id,
                            Content = model.Content,
                            DueDate = model.DueDate,
                            IsImportant = model.IsImportant,
                            IsInMyDay = model.IsInMyDay,
                            HasNote = model.HasNote,
                            HasSteps = model.HasSteps,
                            IsRecurring = model.IsRecurring,
                        }
                    ),
                    NextCursor = 0,
                },
            };
        }

        var appResponseTodoTasks = new List<F13AppResponseModel.BodyModel.TodoTaskModel>();
        for (var i = 0; i < taskCount; i++)
        {
            var taskDetail = foundTodoTasks.ElementAt(i);

            appResponseTodoTasks.Add(
                new()
                {
                    Id = taskDetail.Id,
                    Content = taskDetail.Content,
                    DueDate = taskDetail.DueDate,
                    IsImportant = taskDetail.IsImportant,
                    IsInMyDay = taskDetail.IsInMyDay,
                    HasNote = taskDetail.HasNote,
                    HasSteps = taskDetail.HasSteps,
                    IsRecurring = taskDetail.IsRecurring,
                }
            );
        }
        var nextCursor = appResponseTodoTasks[^1].Id;
        appResponseTodoTasks.RemoveAt(appResponseTodoTasks.Count - 1);

        return new()
        {
            AppCode = F13Constant.AppCode.SUCCESS,
            Body = new() { TodoTasks = appResponseTodoTasks, NextCursor = nextCursor },
        };
    }
}
