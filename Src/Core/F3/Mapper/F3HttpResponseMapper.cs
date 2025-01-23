using System;
using System.Collections.Concurrent;
using F3.Common;
using F3.Models;
using F3.Presentation;
using F3.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F3.Mapper;

public static class F3HttpResponseMapper
{
    private static ConcurrentDictionary<
        F3Constant.AppCode,
        Func<F3AppRequestModel, F3AppResponseModel, HttpContext, F3Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();

            _httpResponseMapper.TryAdd(
                F3Constant.AppCode.EMAIL_ALREADY_EXISTS,
                (appRequest, appResponse, httpContext) =>
                {
                    return F3Constant.DefaultResponse.Http.EMAIL_ALREADY_EXISTS;
                }
            );

            _httpResponseMapper.TryAdd(
                F3Constant.AppCode.PASSWORD_IS_INVALID,
                (appRequest, appResponse, httpContext) =>
                {
                    return F3Constant.DefaultResponse.Http.PASSWORD_IS_INVALID;
                }
            );

            _httpResponseMapper.TryAdd(
                F3Constant.AppCode.SUCCESS,
                (appRequest, appResponse, httpContext) =>
                {
                    return new()
                    {
                        HttpCode = StatusCodes.Status200OK,
                        AppCode = (int)F3Constant.AppCode.SUCCESS,
                    };
                }
            );

            _httpResponseMapper.TryAdd(
                F3Constant.AppCode.SERVER_ERROR,
                (appRequest, appResponse, httpContext) =>
                {
                    return F3Constant.DefaultResponse.Http.SERVER_ERROR;
                }
            );
        }
    }

    public static F3Response Get(
        F3AppRequestModel appRequest,
        F3AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F3StateBag)] as F3StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
