using System;
using System.Collections.Concurrent;
using F8.Common;
using F8.Models;
using F8.Presentation;
using F8.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F8.Mapper;

public static class F8HttpResponseMapper
{
    private static ConcurrentDictionary<
        F8Constant.AppCode,
        Func<F8AppRequestModel, F8AppResponseModel, HttpContext, F8Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F8Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return F8Constant.DefaultResponse.Http.SERVER_ERROR;
            });

        _httpResponseMapper.TryAdd(
            F8Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)F8Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                };
            });

        _httpResponseMapper.TryAdd(
            F8Constant.AppCode.LIST_DOES_NOT_EXIST,
            (appRequest, appResponse, httpContext) =>
            {
                return F8Constant.DefaultResponse.Http.LIST_DOES_NOT_EXIST;
            });
    }

    public static F8Response Get(
        F8AppRequestModel appRequest,
        F8AppResponseModel appResponse,
        HttpContext httpContext)
    {
        Init();

        var stateBag = httpContext.Items[nameof(F8StateBag)] as F8StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
