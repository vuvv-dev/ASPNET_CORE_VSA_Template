using System;
using System.Collections.Concurrent;
using F5.Src.Models;
using F5.Src.Presentation;

namespace F5.Src.Mapper;

public static class F5HttpResponseMapper
{
    private static ConcurrentDictionary<
        int,
        Func<F5AppRequestModel, F5AppResponseModel, F5Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }
    }

    public static F5Response Get(F5AppRequestModel appRequest, F5AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
