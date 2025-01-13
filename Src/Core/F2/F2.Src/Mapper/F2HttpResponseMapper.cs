using System;
using System.Collections.Concurrent;
using F2.Src.Common;
using F2.Src.Models;
using F2.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F2.Src.Mapper;

public static class F2HttpResponseMapper
{
    private static ConcurrentDictionary<
        int,
        Func<F2AppRequestModel, F2AppResponseModel, F2Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();

            _httpResponseMapper.TryAdd(
                F2Constant.AppCode.LIST_NOT_FOUND,
                (appRequest, appResponse) =>
                    new F2Response
                    {
                        HttpCode = StatusCodes.Status404NotFound,
                        AppCode = F2Constant.AppCode.LIST_NOT_FOUND,
                    }
            );

            _httpResponseMapper.TryAdd(
                F2Constant.AppCode.SUCCESS,
                (appRequest, appResponse) =>
                    new F2Response
                    {
                        HttpCode = StatusCodes.Status400BadRequest,
                        AppCode = F2Constant.AppCode.SUCCESS,
                        Body = new()
                        {
                            TodoTaskList = new()
                            {
                                Id = appRequest.ListId,
                                Name = appResponse.Body.Name,
                                CreatedDate = appResponse.Body.CreatedDate,
                            },
                        },
                    }
            );
        }
    }

    public static F2Response Get(F2AppRequestModel appRequest, F2AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
