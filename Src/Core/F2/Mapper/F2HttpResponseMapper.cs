using System;
using System.Collections.Concurrent;
using F2.Common;
using F2.Models;
using F2.Presentation;
using F2.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F2.Mapper;

public static class F2HttpResponseMapper
{
    private static ConcurrentDictionary<
        F2Constant.AppCode,
        Func<F2AppRequestModel, F2AppResponseModel, HttpContext, F2Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();

            _httpResponseMapper.TryAdd(
                F2Constant.AppCode.LIST_NOT_FOUND,
                (appRequest, appResponse, httpContext) =>
                {
                    return F2Constant.DefaultResponse.Http.LIST_NOT_FOUND;
                }
            );

            _httpResponseMapper.TryAdd(
                F2Constant.AppCode.SUCCESS,
                (appRequest, appResponse, httpContext) =>
                {
                    return new()
                    {
                        HttpCode = StatusCodes.Status200OK,
                        AppCode = (int)F2Constant.AppCode.SUCCESS,
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

    public static F2Response Get(
        F2AppRequestModel appRequest,
        F2AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F2StateBag)] as F2StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
