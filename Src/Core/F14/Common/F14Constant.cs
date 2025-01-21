using F14.Models;
using F14.Presentation;
using Microsoft.AspNetCore.Http;

namespace F14.Common;

public static class F14Constant
{
    public const string ENDPOINT_PATH =
        "f14/list/{TodoTaskListId:required}/task/cursor/{TodoTaskId:required}";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class Url
    {
        public static class Query
        {
            public const string NumberOfRecord = "n";
        }
    }

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F14AppResponseModel TASK_NOT_FOUND = new()
            {
                AppCode = AppCode.TASK_NOT_FOUND,
                Body = new() { TodoTasks = [], NextCursor = 0 },
            };

            public static readonly F14AppResponseModel TODO_TASK_LIST_NOT_FOUND = new()
            {
                AppCode = AppCode.TODO_TASK_LIST_NOT_FOUND,
                Body = new() { TodoTasks = [], NextCursor = 0 },
            };
        }

        public static class Http
        {
            public static readonly F14Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F14Response TASK_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.TASK_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
                Body = new() { TodoTasks = [], NextCursor = 0 },
            };

            public static readonly F14Response TODO_TASK_LIST_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.TODO_TASK_LIST_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
                Body = new() { TodoTasks = [], NextCursor = 0 },
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        TASK_NOT_FOUND,

        TODO_TASK_LIST_NOT_FOUND,
    }
}
