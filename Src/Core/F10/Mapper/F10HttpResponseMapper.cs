using System;
using System.Collections.Concurrent;
using System.Linq;
using F10.Common;
using F10.Models;
using F10.Presentation;
using F10.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F10.Mapper;

public static class F10HttpResponseMapper
{
    private static ConcurrentDictionary<
        F10Constant.AppCode,
        Func<F10AppRequestModel, F10AppResponseModel, HttpContext, F10Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F10Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)F10Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new()
                    {
                        TodoTaskLists = appResponse.Body.TodoTaskLists.Select(
                            model => new F10Response.BodyDto.TodoTaskListDto
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
            F10Constant.AppCode.TODO_TASK_LIST_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return F10Constant.DefaultResponse.Http.TODO_TASK_LIST_NOT_FOUND;
            }
        );
    }

    public static F10Response Get(
        F10AppRequestModel appRequest,
        F10AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F10StateBag)] as F10StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
