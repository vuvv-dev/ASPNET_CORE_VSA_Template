using System;
using System.Collections.Concurrent;
using F17.Common;
using F17.Models;
using F17.Presentation;
using F17.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F17.Mapper;

public static class F17HttpResponseMapper
{
    private static ConcurrentDictionary<
        F17Constant.AppCode,
        Func<F17AppRequestModel, F17AppResponseModel, HttpContext, F17Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F17Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)F17Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                };
            }
        );

        _httpResponseMapper.TryAdd(
            F17Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return F17Constant.DefaultResponse.Http.TASK_NOT_FOUND;
            }
        );

        _httpResponseMapper.TryAdd(
            F17Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return F17Constant.DefaultResponse.Http.SERVER_ERROR;
            }
        );
    }

    public static F17Response Get(
        F17AppRequestModel appRequest,
        F17AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F17StateBag)] as F17StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
