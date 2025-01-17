using System;
using System.Collections.Concurrent;
using F8.Src.Common;
using F8.Src.Models;
using F8.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F8.Src.Mapper;

public static class F8HttpResponseMapper
{
    private static ConcurrentDictionary<
        F8Constant.AppCode,
        Func<F8AppRequestModel, F8AppResponseModel, F8Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F8Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F8Constant.DefaultResponse.Http.SERVER_ERROR
        );

        _httpResponseMapper.TryAdd(
            F8Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F8Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                }
        );

        _httpResponseMapper.TryAdd(
            F8Constant.AppCode.LIST_DOES_NOT_EXIST,
            (appRequest, appResponse) => F8Constant.DefaultResponse.Http.LIST_DOES_NOT_EXIST
        );
    }

    public static F8Response Get(F8AppRequestModel appRequest, F8AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
