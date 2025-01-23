using System;
using System.Collections.Concurrent;
using System.Linq;
using F13.Common;
using F13.Models;
using F13.Presentation;
using F13.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F13.Mapper;

public static class F13HttpResponseMapper
{
    private static ConcurrentDictionary<
        F13Constant.AppCode,
        Func<F13AppRequestModel, F13AppResponseModel, HttpContext, F13Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F13Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)F13Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new()
                    {
                        TodoTasks = appResponse.Body.TodoTasks.Select(
                            taskDetail => new F13Response.BodyDto.TodoTaskDto
                            {
                                Id = taskDetail.Id,
                                Content = taskDetail.Content,
                                DueDate = taskDetail.DueDate,
                                IsExpired = taskDetail.IsExpired,
                                IsImportant = taskDetail.IsImportant,
                                IsInMyDay = taskDetail.IsInMyDay,
                                HasNote = taskDetail.HasNote,
                                HasSteps = taskDetail.HasSteps,
                                IsRecurring = taskDetail.IsRecurring,
                            }
                        ),
                        NextCursor = appResponse.Body.NextCursor,
                    },
                };
            }
        );

        _httpResponseMapper.TryAdd(
            F13Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return F13Constant.DefaultResponse.Http.TASK_NOT_FOUND;
            }
        );

        _httpResponseMapper.TryAdd(
            F13Constant.AppCode.TODO_TASK_LIST_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return F13Constant.DefaultResponse.Http.TODO_TASK_LIST_NOT_FOUND;
            }
        );
    }

    public static F13Response Get(
        F13AppRequestModel appRequest,
        F13AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F13StateBag)] as F13StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
