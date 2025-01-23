using System;
using System.Collections.Concurrent;
using F6.Common;
using F6.Models;
using F6.Presentation;
using F6.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F6.Mapper;

public static class F6HttpResponseMapper
{
    private static ConcurrentDictionary<
        F6Constant.AppCode,
        Func<F6AppRequestModel, F6AppResponseModel, HttpContext, F6Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F6Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return F6Constant.DefaultResponse.Http.SERVER_ERROR;
            }
        );

        _httpResponseMapper.TryAdd(
            F6Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = (int)F6Constant.AppCode.SUCCESS,
                    Body = new()
                    {
                        AccessToken = appResponse.Body.AccessToken,
                        RefreshToken = appResponse.Body.RefreshToken,
                    },
                };
            }
        );

        _httpResponseMapper.TryAdd(
            F6Constant.AppCode.REFRESH_TOKEN_DOES_NOT_EXIST,
            (appRequest, appResponse, httpContext) =>
            {
                return F6Constant.DefaultResponse.Http.REFRESH_TOKEN_DOES_NOT_EXIST;
            }
        );

        _httpResponseMapper.TryAdd(
            F6Constant.AppCode.REFRESH_TOKEN_EXPIRED,
            (appRequest, appResponse, httpContext) =>
            {
                return F6Constant.DefaultResponse.Http.REFRESH_TOKEN_EXPIRED;
            }
        );
    }

    public static F6Response Get(
        F6AppRequestModel appRequest,
        F6AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F6StateBag)] as F6StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
