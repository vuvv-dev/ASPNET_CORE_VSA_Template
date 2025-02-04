using System;
using System.Collections.Concurrent;
using F2.Common;
using F2.Models;
using F2.Presentation;
using F2.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F2.Mapper;

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

            _httpResponseMapper.TryAdd(
                Constant.AppCode.LIST_NOT_FOUND,
                (appRequest, appResponse, httpContext) =>
                {
                    return Constant.DefaultResponse.Http.LIST_NOT_FOUND;
                }
            );

            _httpResponseMapper.TryAdd(
                Constant.AppCode.SUCCESS,
                (appRequest, appResponse, httpContext) =>
                {
                    return new()
                    {
                        HttpCode = StatusCodes.Status200OK,
                        AppCode = (int)Constant.AppCode.SUCCESS,
                        Body = new()
                        {
                            TodoTaskList = new()
                            {
                                Id = appResponse.Body.TodoTaskList.Id,
                                Name = appResponse.Body.TodoTaskList.Name,
                            },
                        },
                    };
                }
            );
        }
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
