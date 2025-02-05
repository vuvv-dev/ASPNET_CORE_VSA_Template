using System;
using System.Collections.Concurrent;
using System.Linq;
using F10.Common;
using F10.Models;
using F10.Presentation;
using F10.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F10.Mapper;

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
                        TodoTaskLists = appResponse.Body.TodoTaskLists.Select(
                            model => new Response.BodyDto.TodoTaskListDto
                            {
                                Id = model.Id,
                                Name = model.Name,
                            }
                        ),
                        NextCursor = appResponse.Body.NextCursor,
                    },
                };
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
