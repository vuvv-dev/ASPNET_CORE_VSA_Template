using System;
using System.Collections.Concurrent;
using F9.Common;
using F9.Models;
using F9.Presentation;
using F9.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F9.Mapper;

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
            Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.SERVER_ERROR;
            }
        );

        _httpResponseMapper.TryAdd(
            Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                };
            }
        );

        _httpResponseMapper.TryAdd(
            Constant.AppCode.LIST_DOES_NOT_EXIST,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.LIST_DOES_NOT_EXIST;
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
