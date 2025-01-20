using System;
using System.Collections.Concurrent;
using System.Linq;
using F13.Common;
using F13.Models;
using F13.Presentation;
using Microsoft.AspNetCore.Http;

namespace F13.Mapper;

public static class F13HttpResponseMapper
{
    private static ConcurrentDictionary<
        F13Constant.AppCode,
        Func<F13AppRequestModel, F13AppResponseModel, F13Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F13Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F13Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new()
                    {
                        TodoTasks = appResponse.Body.TodoTasks.Select(
                            model => new F13Response.BodyDto.TodoTaskDto
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
                        NextCursor = appResponse.Body.NextCursor,
                    },
                }
        );

        _httpResponseMapper.TryAdd(
            F13Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F13Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F13Constant.AppCode.TODO_TASK_LIST_NOT_FOUND,
            (appRequest, appResponse) => F13Constant.DefaultResponse.Http.TODO_TASK_LIST_NOT_FOUND
        );
    }

    public static F13Response Get(F13AppRequestModel appRequest, F13AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
