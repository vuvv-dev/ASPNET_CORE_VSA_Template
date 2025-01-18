using System;
using System.Collections.Concurrent;
using System.Linq;
using F10.Src.Common;
using F10.Src.Models;
using F10.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F10.Src.Mapper;

public static class F10HttpResponseMapper
{
    private static ConcurrentDictionary<
        F10Constant.AppCode,
        Func<F10AppRequestModel, F10AppResponseModel, F10Response>
    > _httpResponseMapper;

    private static void Init()
    {
        if (Equals(_httpResponseMapper, null))
        {
            _httpResponseMapper = new();
        }

        _httpResponseMapper.TryAdd(
            F10Constant.AppCode.SUCCESS,
            (appRequest, appResponse) =>
                new()
                {
                    AppCode = (int)F10Constant.AppCode.SUCCESS,
                    HttpCode = StatusCodes.Status200OK,
                    Body = new()
                    {
                        TodoTaskLists = appResponse.Body.TodoTaskLists.Select(
                            model => new F10Response.BodyDto.TodoTaskListDto
                            {
                                Id = model.Id,
                                Name = model.Name,
                            }
                        ),
                        NextCursor = appResponse.Body.NextCursor,
                    },
                }
        );
    }

    public static F10Response Get(F10AppRequestModel appRequest, F10AppResponseModel appResponse)
    {
        Init();

        return _httpResponseMapper[appResponse.AppCode](appRequest, appResponse);
    }
}
