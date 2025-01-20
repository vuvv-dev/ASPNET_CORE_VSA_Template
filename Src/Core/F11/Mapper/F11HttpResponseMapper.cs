using System;
using System.Collections.Concurrent;
using F11.Common;
using F11.Models;
using F11.Presentation;
using Microsoft.AspNetCore.Http;

namespace F11.Mapper;

public static class F11HttpResponseMapper
{
    private static ConcurrentDictionary<
        F11Constant.AppCode,
        Func<F11AppRequestModel, F11AppResponseModel, F11Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F11Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F11Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new() { TodoTaskId = appResponse.Body.TodoTaskId },
                }
        );

        _httpResponseMapper.TryAdd(
            F11Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F11Constant.DefaultResponse.Http.SERVER_ERROR
        );

        _httpResponseMapper.TryAdd(
            F11Constant.AppCode.TODO_TASK_LIST_NOT_FOUND,
            (appRequest, appResponse) => F11Constant.DefaultResponse.Http.TODO_TASK_LIST_NOT_FOUND
        );
    }

    public static F11Response Get(F11AppRequestModel appRequest, F11AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
