using System;
using System.Collections.Concurrent;
using F5.Common;
using F5.Models;
using F5.Presentation;
using F5.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F5.Mapper;

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
            Constant.AppCode.PASSWORD_IS_INVALID,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.PASSWORD_IS_INVALID;
            }
        );

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
                };
            }
        );

        _httpResponseMapper.TryAdd(
            Constant.AppCode.TOKEN_DOES_NOT_EXIST,
            (appRequest, appResponse, httpContext) =>
            {
                return Constant.DefaultResponse.Http.TOKEN_DOES_NOT_EXIST;
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
