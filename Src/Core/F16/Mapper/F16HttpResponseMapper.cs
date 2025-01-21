using System;
using System.Collections.Concurrent;
using F16.Common;
using F16.Models;
using F16.Presentation;
using Microsoft.AspNetCore.Http;

namespace F16.Mapper;

public static class F16HttpResponseMapper
{
    private static ConcurrentDictionary<
        F16Constant.AppCode,
        Func<F16AppRequestModel, F16AppResponseModel, F16Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F16Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F16Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                }
        );

        _httpResponseMapper.TryAdd(
            F16Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F16Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F16Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F16Constant.DefaultResponse.Http.SERVER_ERROR
        );
    }

    public static F16Response Get(F16AppRequestModel appRequest, F16AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
