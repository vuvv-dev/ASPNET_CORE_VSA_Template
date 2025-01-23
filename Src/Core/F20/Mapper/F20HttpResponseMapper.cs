using System;
using System.Collections.Concurrent;
using F20.Common;
using F20.Models;
using F20.Presentation;
using F20.Presentation.Filters.SetStateBag;
using Microsoft.AspNetCore.Http;

namespace F20.Mapper;

public static class F20HttpResponseMapper
{
    private static ConcurrentDictionary<
        F20Constant.AppCode,
        Func<F20AppRequestModel, F20AppResponseModel, HttpContext, F20Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F20Constant.AppCode.SUCCESS,
            (appRequest, appResponse, httpContext) =>
            {
                return new()
                {
                    AppCode = (int)F20Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                };
            }
        );

        _httpResponseMapper.TryAdd(
            F20Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse, httpContext) =>
            {
                return F20Constant.DefaultResponse.Http.TASK_NOT_FOUND;
            }
        );

        _httpResponseMapper.TryAdd(
            F20Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse, httpContext) =>
            {
                return F20Constant.DefaultResponse.Http.SERVER_ERROR;
            }
        );
    }

    public static F20Response Get(
        F20AppRequestModel appRequest,
        F20AppResponseModel appResponse,
        HttpContext httpContext
    )
    {
        Init();

        var stateBag = httpContext.Items[nameof(F20StateBag)] as F20StateBag;

        var httpResponse = _httpResponseMapper[appResponse.AppCode]
            (appRequest, appResponse, httpContext);
        stateBag.HttpResponse = httpResponse;

        return httpResponse;
    }
}
