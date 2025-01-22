using System;
using System.Collections.Concurrent;
using F15.Common;
using F15.Models;
using F15.Presentation;
using Microsoft.AspNetCore.Http;

namespace F15.Mapper;

public static class F15HttpResponseMapper
{
    private static ConcurrentDictionary<
        F15Constant.AppCode,
        Func<F15AppRequestModel, F15AppResponseModel, F15Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F15Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F15Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new()
                    {
                        TodoTask = new()
                        {
                            Id = appResponse.Body.TodoTask.Id,
                            Content = appResponse.Body.TodoTask.Content,
                            DueDate = appResponse.Body.TodoTask.DueDate,
                            IsCompleted = appResponse.Body.TodoTask.IsCompleted,
                            IsImportant = appResponse.Body.TodoTask.IsImportant,
                            IsInMyDay = appResponse.Body.TodoTask.IsInMyDay,
                            Note = appResponse.Body.TodoTask.Note,
                        },
                    },
                }
        );

        _httpResponseMapper.TryAdd(
            F15Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F15Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );
    }

    public static F15Response Get(F15AppRequestModel appRequest, F15AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
