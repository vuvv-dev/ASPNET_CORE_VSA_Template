using System;
using System.Collections.Concurrent;
using System.Linq;
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
                    new()
                    {
                        HttpCode = StatusCodes.Status404NotFound,
                        AppCode = F2Constant.AppCode.LIST_NOT_FOUND,
                    }
            );

            _httpResponseMapper.TryAdd(
                F2Constant.AppCode.SUCCESS,
                (appRequest, appResponse) =>
                    new()
                    {
                        HttpCode = StatusCodes.Status400BadRequest,
                        AppCode = F2Constant.AppCode.SUCCESS,
                        Body = new()
                        {
                            TodoTaskList = new()
                            {
                                Id = appResponse.Body.Id,
                                Name = appResponse.Body.Name,
                                TodoTasks = appResponse.Body.TodoTasks.Select(
                                    model => new F2Response.BodyDto.TodoTaskListDto.TodoTaskDto
                                    {
                                        Id = model.Id,
                                        Name = model.Name,
                                        DueDate = model.DueDate,
                                        IsInMyDay = model.IsInMyDay,
                                        IsImportant = model.IsImportant,
                                        IsFinished = model.IsFinished,
                                    }
                                ),
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
