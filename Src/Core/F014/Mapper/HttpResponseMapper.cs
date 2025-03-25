using System;
using System.Collections.Concurrent;
using System.Linq;
using F014.Common;
using F014.Models;
using F014.Presentation;
using F014.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F014.Mapper;

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
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new()
                    {
                        TodoTasks = appResponse.Body.TodoTasks.Select(
                            taskDetail => new Response.BodyDto.TodoTaskDto
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
                        ),
                        NextCursor = appResponse.Body.NextCursor,
                    },
                };
            }
        );

        _httpResponseMapper.TryAdd(
            Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.TASK_NOT_FOUND;
            }
        );

        _httpResponseMapper.TryAdd(
            Constant.AppCode.TODO_TASK_LIST_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.TODO_TASK_LIST_NOT_FOUND;
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
