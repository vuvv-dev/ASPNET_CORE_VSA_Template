using System;
using System.Collections.Concurrent;
using F4.Src.Models;
using F4.Src.Presentation;

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
    }

    public static F4Response Get(F4AppRequestModel appRequest, F4AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
