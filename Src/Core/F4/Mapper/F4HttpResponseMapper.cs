using System;
using System.Collections.Concurrent;
using F4.Common;
using F4.Models;
using F4.Presentation;
using F4.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F4.Mapper;

public static class F4HttpResponseMapper
{
    private static ConcurrentDictionary<
        F4Constant.AppCode,
        Func<F4AppRequestModel, F4AppResponseModel, HttpContext, F4Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F4Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return F4Constant.DefaultResponse.Http.SERVER_ERROR;
            }
        );

        _httpResponseMapper.TryAdd(
            F4Constant.AppCode.USER_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return F4Constant.DefaultResponse.Http.USER_NOT_FOUND;
            }
        );

        _httpResponseMapper.TryAdd(
            F4Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = (int)F4Constant.AppCode.SUCCESS,
                    Body = new() { ResetPasswordToken = appResponse.Body.ResetPasswordToken },
                };
            }
        );
    }

    public static F4Response Get(
        F4AppRequestModel appRequest,
        F4AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F4StateBag)] as F4StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
