using System;
using System.Collections.Concurrent;
using F15.Common;
using F15.Models;
using F15.Presentation;
using F15.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F15.Mapper;

public static class HttpResponseMapper
{
    private static ConcurrentDictionary<
        Constant.AppCode,
        Func<AppRequestModel, AppResponseModel, HttpContext, Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContex) =>
            {
                return new()
                {
                    AppCode = (int)Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new()
                    {
                        TodoTask = new()
                        {
                            Id = appResponse.Body.TodoTask.Id,
                            Content = appResponse.Body.TodoTask.Content,
                            DueDate = appResponse.Body.TodoTask.DueDate,
                            IsExpired = appResponse.Body.TodoTask.IsExpired,
                            IsCompleted = appResponse.Body.TodoTask.IsCompleted,
                            IsImportant = appResponse.Body.TodoTask.IsImportant,
                            IsInMyDay = appResponse.Body.TodoTask.IsInMyDay,
                            Note = appResponse.Body.TodoTask.Note,
                        },
                    },
                };
            }
        );

        _httpResponseMapper.TryAdd(
            Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse, httpContex) =>
            {
                return Constant.DefaultResponse.Http.TASK_NOT_FOUND;
            }
        );
    }

    public static Response Get(
        AppRequestModel appRequest,
        AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(StateBag)] as StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
