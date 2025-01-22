using System;
using System.Collections.Concurrent;
using F21.Common;
using F21.Models;
using F21.Presentation;
using Microsoft.AspNetCore.Http;

namespace F21.Mapper;

public static class F21HttpResponseMapper
{
    private static ConcurrentDictionary<
        F21Constant.AppCode,
        Func<F21AppRequestModel, F21AppResponseModel, F21Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F21Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F21Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                }
        );

        _httpResponseMapper.TryAdd(
            F21Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F21Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F21Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F21Constant.DefaultResponse.Http.SERVER_ERROR
        );
    }

    public static F21Response Get(F21AppRequestModel appRequest, F21AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
