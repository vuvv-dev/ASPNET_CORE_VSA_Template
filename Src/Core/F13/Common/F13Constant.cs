using F13.Models;
using F13.Presentation;
using Microsoft.AspNetCore.Http;

namespace F13.Common;

public static class F13Constant
{
    public const string ENDPOINT_PATH =
        "f13/list/{TodoTaskListId:required}/task/cursor/{TodoTaskId:required}";

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
            public static readonly F13AppResponseModel TASK_NOT_FOUND = new()
            {
                AppCode = AppCode.TASK_NOT_FOUND,
            };

            public static readonly F13AppResponseModel TODO_TASK_LIST_NOT_FOUND = new()
            {
                AppCode = AppCode.TODO_TASK_LIST_NOT_FOUND,
            };
        }

        public static class Http
        {
            public static readonly F13Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F13Response TASK_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.TASK_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
            };

            public static readonly F13Response TODO_TASK_LIST_NOT_FOUND = new()
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
