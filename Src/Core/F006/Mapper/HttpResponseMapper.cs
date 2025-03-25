using System;
using System.Collections.Concurrent;
using F006.Common;
using F006.Models;
using F006.Presentation;
using F006.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F006.Mapper;

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
            Constant.AppCode.REFRESH_TOKEN_DOES_NOT_EXIST,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.REFRESH_TOKEN_DOES_NOT_EXIST;
            }
        );

        _httpResponseMapper.TryAdd(
            Constant.AppCode.REFRESH_TOKEN_EXPIRED,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.REFRESH_TOKEN_EXPIRED;
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
