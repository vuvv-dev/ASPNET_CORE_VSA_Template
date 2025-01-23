using System;
using System.Collections.Concurrent;
using F1.Common;
using F1.Models;
using F1.Presentation;
using F1.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F1.Mapper;

public static class F1HttpResponseMapper
{
    private static ConcurrentDictionary<
        F1Constant.AppCode,
        Func<F1AppRequestModel, F1AppResponseModel, HttpContext, F1Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();

            _httpResponseMapper.TryAdd(
                F1Constant.AppCode.PASSWORD_IS_INCORRECT,
                (appRequest, appResponse, httpContext) =>
                {
                    return F1Constant.DefaultResponse.Http.PASSWORD_IS_INCORRECT;
                }
            );

            _httpResponseMapper.TryAdd(
                F1Constant.AppCode.TEMPORARY_BANNED,
                (appRequest, appResponse, httpContext) =>
                {
                    return F1Constant.DefaultResponse.Http.TEMPORARY_BANNED;
                }
            );

            _httpResponseMapper.TryAdd(
                F1Constant.AppCode.SUCCESS,
                (appRequest, appResponse, httpContext) =>
                {
                    return new()
                    {
                        HttpCode = StatusCodes.Status200OK,
                        AppCode = (int)F1Constant.AppCode.SUCCESS,
                        Body = new()
                        {
                            AccessToken = appResponse.Body.AccessToken,
                            RefreshToken = appResponse.Body.RefreshToken,
                        },
                    };
                }
            );

            _httpResponseMapper.TryAdd(
                F1Constant.AppCode.USER_NOT_FOUND,
                (appRequest, appResponse, httpContext) =>
                {
                    return F1Constant.DefaultResponse.Http.USER_NOT_FOUND;
                }
            );

            _httpResponseMapper.TryAdd(
                F1Constant.AppCode.SERVER_ERROR,
                (appRequest, appResponse, httpContext) =>
                {
                    return F1Constant.DefaultResponse.Http.SERVER_ERROR;
                }
            );
        }
    }

    public static F1Response Get(
        F1AppRequestModel appRequest,
        F1AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F1StateBag)] as F1StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode](appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
