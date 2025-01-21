using System;
using System.Collections.Concurrent;
using F17.Common;
using F17.Models;
using F17.Presentation;
using Microsoft.AspNetCore.Http;

namespace F17.Mapper;

public static class F17HttpResponseMapper
{
    private static ConcurrentDictionary<
        F17Constant.AppCode,
        Func<F17AppRequestModel, F17AppResponseModel, F17Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F17Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F17Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                }
        );

        _httpResponseMapper.TryAdd(
            F17Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F17Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F17Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F17Constant.DefaultResponse.Http.SERVER_ERROR
        );
    }

    public static F17Response Get(F17AppRequestModel appRequest, F17AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
