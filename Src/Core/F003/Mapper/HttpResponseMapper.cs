using System;
using System.Collections.Concurrent;
using F003.Common;
using F003.Models;
using F003.Presentation;
using F003.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F003.Mapper;

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
                Constant.AppCode.EMAIL_ALREADY_EXISTS,
                (appRequest, appResponse, httpContext) =>
                {
                    return Constant.DefaultResponse.Http.EMAIL_ALREADY_EXISTS;
                }
            );

            _httpResponseMapper.TryAdd(
                Constant.AppCode.PASSWORD_IS_INVALID,
                (appRequest, appResponse, httpContext) =>
                {
                    return Constant.DefaultResponse.Http.PASSWORD_IS_INVALID;
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
                    };
                }
            );

            _httpResponseMapper.TryAdd(
                Constant.AppCode.SERVER_ERROR,
                (appRequest, appResponse, httpContext) =>
                {
                    return Constant.DefaultResponse.Http.SERVER_ERROR;
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
