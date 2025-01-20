using F11.Models;
using F11.Presentation;
using Microsoft.AspNetCore.Http;

namespace F11.Common;

public static class F11Constant
{
    public const string ENDPOINT_PATH = "f11";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F11AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly F11AppResponseModel TODO_TASK_LIST_NOT_FOUND = new()
            {
                AppCode = AppCode.TODO_TASK_LIST_NOT_FOUND,
            };
        }

        public static class Http
        {
            public static readonly F11Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F11Response SERVER_ERROR = new()
            {
                AppCode = (int)AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };

            public static readonly F11Response TODO_TASK_LIST_NOT_FOUND = new()
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

        SERVER_ERROR,

        TODO_TASK_LIST_NOT_FOUND,
    }
}
