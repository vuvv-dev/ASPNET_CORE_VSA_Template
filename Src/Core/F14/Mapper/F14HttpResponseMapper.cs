using System;
using System.Collections.Concurrent;
using System.Linq;
using F14.Common;
using F14.Models;
using F14.Presentation;
using Microsoft.AspNetCore.Http;

namespace F14.Mapper;

public static class F14HttpResponseMapper
{
    private static ConcurrentDictionary<
        F14Constant.AppCode,
        Func<F14AppRequestModel, F14AppResponseModel, F14Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F14Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F14Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new()
                    {
                        TodoTasks = appResponse.Body.TodoTasks.Select(
                            taskDetail => new F14Response.BodyDto.TodoTaskDto
                            {
                                Id = taskDetail.Id,
                                Content = taskDetail.Content,
                                IsCompleted = taskDetail.IsCompleted,
                                DueDate = taskDetail.DueDate,
                                IsImportant = taskDetail.IsImportant,
                                IsInMyDay = taskDetail.IsInMyDay,
                                HasNote = taskDetail.HasNote,
                                HasSteps = taskDetail.HasSteps,
                                IsRecurring = taskDetail.IsRecurring,
                            }
                        ),
                        NextCursor = appResponse.Body.NextCursor,
                    },
                }
        );

        _httpResponseMapper.TryAdd(
            F14Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F14Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F14Constant.AppCode.TODO_TASK_LIST_NOT_FOUND,
            (appRequest, appResponse) => F14Constant.DefaultResponse.Http.TODO_TASK_LIST_NOT_FOUND
        );
    }

    public static F14Response Get(F14AppRequestModel appRequest, F14AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
