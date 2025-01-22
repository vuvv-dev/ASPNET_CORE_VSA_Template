using System;
using System.Collections.Concurrent;
using F18.Common;
using F18.Models;
using F18.Presentation;
using Microsoft.AspNetCore.Http;

namespace F17.Mapper;

public static class F17HttpResponseMapper
{
    private static ConcurrentDictionary<
        F18Constant.AppCode,
        Func<F18AppRequestModel, F18AppResponseModel, F18Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F18Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F18Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                }
        );

        _httpResponseMapper.TryAdd(
            F18Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F18Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F18Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F18Constant.DefaultResponse.Http.SERVER_ERROR
        );
    }

    public static F18Response Get(F18AppRequestModel appRequest, F18AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
