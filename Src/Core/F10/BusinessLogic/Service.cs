using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F10.Common;
using F10.DataAccess;
using F10.Models;
using FCommon.FeatureService;

namespace F10.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;

    public Service(Lazy<IRepository> repository)
    {
        _repository = repository;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        if (request.TodoTaskListId != 0)
        {
            var doesListExtst = await _repository.Value.DoesTodoTaskListExistAsync(
                request.TodoTaskListId,
                ct
            );
            if (!doesListExtst)
            {
                return Constant.DefaultResponse.App.TODO_TASK_LIST_NOT_FOUND;
            }
        }

        var foundTodoTaskLists = await _repository.Value.GetTodoTaskListAsync(
            request.TodoTaskListId,
            request.NumberOfRecord,
            ct
        );

        var listCount = foundTodoTaskLists.Count();
        if (listCount <= request.NumberOfRecord)
        {
            return new()
            {
                AppCode = Constant.AppCode.SUCCESS,
                Body = new()
                {
                    TodoTaskLists = foundTodoTaskLists.Select(
                        model => new AppResponseModel.BodyModel.TodoTaskListModel
                        {
                            Id = model.Id,
                            Name = model.Name,
                        }
                    ),
                    NextCursor = 0,
                },
            };
        }

        var appResponseTodoTaskLists = new List<AppResponseModel.BodyModel.TodoTaskListModel>();
        for (var i = 0; i < listCount; i++)
        {
            var listDetail = foundTodoTaskLists.ElementAt(i);

            appResponseTodoTaskLists.Add(new() { Id = listDetail.Id, Name = listDetail.Name });
        }
        var nextCursor = appResponseTodoTaskLists[^1].Id;
        appResponseTodoTaskLists.RemoveAt(appResponseTodoTaskLists.Count - 1);

        return new()
        {
            AppCode = Constant.AppCode.SUCCESS,
            Body = new() { TodoTaskLists = appResponseTodoTaskLists, NextCursor = nextCursor },
        };
    }
}
