using System;
using System.Collections.Concurrent;
using F004.Common;
using F004.Models;
using F004.Presentation;
using F004.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F004.Mapper;

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
            Constant.AppCode.USER_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.USER_NOT_FOUND;
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
                    Body = new() { ResetPasswordToken = appResponse.Body.ResetPasswordToken },
                };
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
