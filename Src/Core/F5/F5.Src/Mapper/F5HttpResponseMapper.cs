using System;
using System.Collections.Concurrent;
using F5.Src.Common;
using F5.Src.Models;
using F5.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F5.Src.Mapper;

public static class F5HttpResponseMapper
{
    private static ConcurrentDictionary<
        F5Constant.AppCode,
        Func<F5AppRequestModel, F5AppResponseModel, F5Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F5Constant.AppCode.PASSWORD_IS_INVALID,
            (appRequest, appResponse) => F5Constant.DefaultResponse.Http.PASSWORD_IS_INVALID
        );

        _httpResponseMapper.TryAdd(
            F5Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F5Constant.DefaultResponse.Http.SERVER_ERROR
        );

        _httpResponseMapper.TryAdd(
            F5Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = (int)F5Constant.AppCode.SUCCESS,
                }
        );

        _httpResponseMapper.TryAdd(
            F5Constant.AppCode.TOKEN_DOES_NOT_EXIST,
            (appRequest, appResponse) => F5Constant.DefaultResponse.Http.TOKEN_DOES_NOT_EXIST
        );
    }

    public static F5Response Get(F5AppRequestModel appRequest, F5AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
