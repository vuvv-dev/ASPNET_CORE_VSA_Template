using System;
using System.Collections.Concurrent;
using F13.Common;
using F13.Models;
using F13.Presentation;
using Microsoft.AspNetCore.Http;

namespace F13.Mapper;

public static class F13HttpResponseMapper
{
    private static ConcurrentDictionary<
        F13Constant.AppCode,
        Func<F13AppRequestModel, F13AppResponseModel, F13Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }
    }

    public static F13Response Get(F13AppRequestModel appRequest, F13AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
