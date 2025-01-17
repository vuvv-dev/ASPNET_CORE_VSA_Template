using System;
using System.Collections.Concurrent;
using F9.Src.Common;
using F9.Src.Models;
using F9.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F9.Src.Mapper;

public static class F9HttpResponseMapper
{
    private static ConcurrentDictionary<
        F9Constant.AppCode,
        Func<F9AppRequestModel, F9AppResponseModel, F9Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F9Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F9Constant.DefaultResponse.Http.SERVER_ERROR
        );

        _httpResponseMapper.TryAdd(
            F9Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F9Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                }
        );

        _httpResponseMapper.TryAdd(
            F9Constant.AppCode.LIST_DOES_NOT_EXIST,
            (appRequest, appResponse) => F9Constant.DefaultResponse.Http.LIST_DOES_NOT_EXIST
        );
    }

    public static F9Response Get(F9AppRequestModel appRequest, F9AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
