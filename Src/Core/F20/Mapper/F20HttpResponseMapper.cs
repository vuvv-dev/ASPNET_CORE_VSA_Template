using System;
using System.Collections.Concurrent;
using F20.Common;
using F20.Models;
using F20.Presentation;
using Microsoft.AspNetCore.Http;

namespace F20.Mapper;

public static class F20HttpResponseMapper
{
    private static ConcurrentDictionary<
        F20Constant.AppCode,
        Func<F20AppRequestModel, F20AppResponseModel, F20Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F20Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F20Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                }
        );

        _httpResponseMapper.TryAdd(
            F20Constant.AppCode.TASK_NOT_FOUND,
            (appRequest, appResponse) => F20Constant.DefaultResponse.Http.TASK_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F20Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F20Constant.DefaultResponse.Http.SERVER_ERROR
        );
    }

    public static F20Response Get(F20AppRequestModel appRequest, F20AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
