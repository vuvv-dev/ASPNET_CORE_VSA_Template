using System;
using System.Collections.Concurrent;
using F4.Src.Common;
using F4.Src.Models;
using F4.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F4.Src.Mapper;

public static class F4HttpResponseMapper
{
    private static ConcurrentDictionary<
        int,
        Func<F4AppRequestModel, F4AppResponseModel, F4Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F4Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F4Constant.DefaultResponse.Http.SERVER_ERROR
        );

        _httpResponseMapper.TryAdd(
            F4Constant.AppCode.USER_NOT_FOUND,
            (appRequest, appResponse) => F4Constant.DefaultResponse.Http.USER_NOT_FOUND
        );

        _httpResponseMapper.TryAdd(
            F4Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = F4Constant.AppCode.SUCCESS,
                    Body = new() { Token = appResponse.Body.Token },
                }
        );
    }

    public static F4Response Get(F4AppRequestModel appRequest, F4AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
