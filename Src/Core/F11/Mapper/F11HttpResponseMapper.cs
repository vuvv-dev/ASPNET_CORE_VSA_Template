using System;
using System.Collections.Concurrent;
using F11.Common;
using F11.Models;
using F11.Presentation;

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
    }

    public static F11Response Get(F11AppRequestModel appRequest, F11AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
