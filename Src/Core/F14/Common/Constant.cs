using F14.Models;
using F14.Presentation;
using Microsoft.AspNetCore.Http;

namespace F14.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F14Endpoint";

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
            public static readonly AppResponseModel TASK_NOT_FOUND = new()
            {
                AppCode = AppCode.TASK_NOT_FOUND,
            };

            public static readonly AppResponseModel TODO_TASK_LIST_NOT_FOUND = new()
            {
                AppCode = AppCode.TODO_TASK_LIST_NOT_FOUND,
            };
        }

        public static class Http
        {
            public static readonly Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly Response TASK_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.TASK_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
            };

            public static readonly Response TODO_TASK_LIST_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.TODO_TASK_LIST_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
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
