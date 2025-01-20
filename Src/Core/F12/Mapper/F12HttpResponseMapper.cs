using System;
using System.Collections.Concurrent;
using F12.Common;
using F12.Models;
using F12.Presentation;
using Microsoft.AspNetCore.Http;

namespace F12.Mapper;

public static class F12HttpResponseMapper
{
    private static ConcurrentDictionary<
        F12Constant.AppCode,
        Func<F12AppRequestModel, F12AppResponseModel, F12Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F12Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F12Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new(),
                }
        );

        _httpResponseMapper.TryAdd(
            F12Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F12Constant.DefaultResponse.Http.SERVER_ERROR
        );

        _httpResponseMapper.TryAdd(
            F12Constant.AppCode.TODO_TASK_LIST_NOT_FOUND,
            (appRequest, appResponse) => F12Constant.DefaultResponse.Http.TODO_TASK_LIST_NOT_FOUND
        );
    }

    public static F12Response Get(F12AppRequestModel appRequest, F12AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
