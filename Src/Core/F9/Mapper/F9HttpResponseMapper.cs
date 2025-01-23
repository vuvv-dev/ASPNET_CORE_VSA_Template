using System;
using System.Collections.Concurrent;
using F9.Common;
using F9.Models;
using F9.Presentation;
using F9.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F9.Mapper;

public static class F9HttpResponseMapper
{
    private static ConcurrentDictionary<
        F9Constant.AppCode,
        Func<F9AppRequestModel, F9AppResponseModel, HttpContext, F9Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F9Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return F9Constant.DefaultResponse.Http.SERVER_ERROR;
            }
        );

        _httpResponseMapper.TryAdd(
            F9Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)F9Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                };
            }
        );

        _httpResponseMapper.TryAdd(
            F9Constant.AppCode.LIST_DOES_NOT_EXIST,
            (appRequest, appResponse, httpContext) =>
            {
                return F9Constant.DefaultResponse.Http.LIST_DOES_NOT_EXIST;
            }
        );
    }

    public static F9Response Get(
        F9AppRequestModel appRequest,
        F9AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F9StateBag)] as F9StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
