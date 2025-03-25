using System;
using System.Collections.Concurrent;
using F001.Common;
using F001.Models;
using F001.Presentation;
using F001.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F001.Mapper;

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
                Constant.AppCode.PASSWORD_IS_INCORRECT,
                (appRequest, appResponse, httpContext) =>
                {
                    return Constant.DefaultResponse.Http.PASSWORD_IS_INCORRECT;
                }
            );

            _httpResponseMapper.TryAdd(
                Constant.AppCode.TEMPORARY_BANNED,
                (appRequest, appResponse, httpContext) =>
                {
                    return Constant.DefaultResponse.Http.TEMPORARY_BANNED;
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
                        Body = new()
                        {
                            AccessToken = appResponse.Body.AccessToken,
                            RefreshToken = appResponse.Body.RefreshToken,
                        },
                    };
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
