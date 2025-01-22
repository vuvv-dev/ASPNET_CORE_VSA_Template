using System;
using System.Collections.Concurrent;
using F19.Common;
using F19.Models;
using F19.Presentation;
using Microsoft.AspNetCore.Http;

namespace F19.Mapper;

public static class F19HttpResponseMapper
{
    private static ConcurrentDictionary<
        F19Constant.AppCode,
        Func<F19AppRequestModel, F19AppResponseModel, F19Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F19Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F19Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                }
        );

        _httpResponseMapper.TryAdd(
            F19Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F19Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F19Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F19Constant.DefaultResponse.Http.SERVER_ERROR
        );
    }

    public static F19Response Get(F19AppRequestModel appRequest, F19AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
