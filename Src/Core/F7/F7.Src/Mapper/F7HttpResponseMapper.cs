using System;
using System.Collections.Concurrent;
using F7.Src.Common;
using F7.Src.Models;
using F7.Src.Presentation;

namespace F7.Src.Mapper;

public static class F7HttpResponseMapper
{
    private static ConcurrentDictionary<
        int,
        Func<F7AppRequestModel, F7AppResponseModel, F7Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F7Constant.AppCode.SERVER_ERROR,
            (appRequest, appResponse) => F7Constant.DefaultResponse.Http.SERVER_ERROR
        );
    }

    public static F7Response Get(F7AppRequestModel appRequest, F7AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
