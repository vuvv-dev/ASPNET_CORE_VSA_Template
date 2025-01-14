using System;
using System.Collections.Concurrent;
using F3.Src.Common;
using F3.Src.Models;
using F3.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F3.Src.Mapper;

public static class F3HttpResponseMapper
{
    private static ConcurrentDictionary<
        int,
        Func<F3AppRequestModel, F3AppResponseModel, F3Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();

            _httpResponseMapper.TryAdd(
                F3Constant.AppCode.EMAIL_ALREADY_EXISTS,
                (appRequest, appResponse) => F3Constant.DefaultResponse.Http.EMAIL_ALREADY_EXISTS
            );

            _httpResponseMapper.TryAdd(
                F3Constant.AppCode.PASSWORD_IS_INVALID,
                (appRequest, appResponse) => F3Constant.DefaultResponse.Http.PASSWORD_IS_INVALID
            );

            _httpResponseMapper.TryAdd(
                F3Constant.AppCode.SUCCESS,
                (appRequest, appResponse) =>
                    new()
                    {
                        HttpCode = StatusCodes.Status200OK,
                        AppCode = F3Constant.AppCode.SUCCESS,
                    }
            );

            _httpResponseMapper.TryAdd(
                F3Constant.AppCode.SERVER_ERROR,
                (appRequest, appResponse) => F3Constant.DefaultResponse.Http.SERVER_ERROR
            );
        }
    }

    public static F3Response Get(F3AppRequestModel appRequest, F3AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
