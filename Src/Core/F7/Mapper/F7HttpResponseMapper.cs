using System;
using System.Collections.Concurrent;
using F7.Common;
using F7.Models;
using F7.Presentation;
using F7.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F7.Mapper;

public static class F7HttpResponseMapper
{
    private static ConcurrentDictionary<
        F7Constant.AppCode,
        Func<F7AppRequestModel, F7AppResponseModel, HttpContext, F7Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F7Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return F7Constant.DefaultResponse.Http.SERVER_ERROR;
            }
        );

        _httpResponseMapper.TryAdd(
            F7Constant.AppCode.LIST_ALREADY_EXISTS,
            (appRequest, appResponse, httpContext) =>
            {
                return F7Constant.DefaultResponse.Http.LIST_ALREADY_EXISTS;
            }
        );

        _httpResponseMapper.TryAdd(
            F7Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)F7Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status201Created,
                    Body = new() { ListId = appResponse.Body.ListId },
                };
            }
        );
    }

    public static F7Response Get(
        F7AppRequestModel appRequest,
        F7AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F7StateBag)] as F7StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
