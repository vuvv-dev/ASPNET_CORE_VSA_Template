using System;
using System.Collections.Concurrent;
using F5.Common;
using F5.Models;
using F5.Presentation;
using F5.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F5.Mapper;

public static class F5HttpResponseMapper
{
    private static ConcurrentDictionary<
        F5Constant.AppCode,
        Func<F5AppRequestModel, F5AppResponseModel, HttpContext, F5Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F5Constant.AppCode.PASSWORD_IS_INVALID,
            (appRequest, appResponse, httpContext) =>
            {
                return F5Constant.DefaultResponse.Http.PASSWORD_IS_INVALID;
            }
        );

        _httpResponseMapper.TryAdd(
            F5Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return F5Constant.DefaultResponse.Http.SERVER_ERROR;
            }
        );

        _httpResponseMapper.TryAdd(
            F5Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = (int)F5Constant.AppCode.SUCCESS,
                };
            }
        );

        _httpResponseMapper.TryAdd(
            F5Constant.AppCode.TOKEN_DOES_NOT_EXIST,
            (appRequest, appResponse, httpContext) =>
            {
                return F5Constant.DefaultResponse.Http.TOKEN_DOES_NOT_EXIST;
            }
        );
    }

    public static F5Response Get(
        F5AppRequestModel appRequest,
        F5AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F5StateBag)] as F5StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
