using System;
using System.Collections.Concurrent;
using F6.Src.Common;
using F6.Src.Models;
using F6.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F6.Src.Mapper;

public static class F6HttpResponseMapper
{
    private static ConcurrentDictionary<
        int,
        Func<F6AppRequestModel, F6AppResponseModel, F6Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F6Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F6Constant.DefaultResponse.Http.SERVER_ERROR
        );

        _httpResponseMapper.TryAdd(
            F6Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new() { HttpCode = StatusCodes.Status200OK, AppCode = F6Constant.AppCode.SUCCESS }
        );

        _httpResponseMapper.TryAdd(
            F6Constant.AppCode.TOKEN_DOES_NOT_EXIST,
            (appRequest, appResponse) => F6Constant.DefaultResponse.Http.TOKEN_DOES_NOT_EXIST
        );
    }

    public static F6Response Get(F6AppRequestModel appRequest, F6AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
