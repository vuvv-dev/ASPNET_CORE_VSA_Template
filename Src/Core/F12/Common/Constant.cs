using F12.Models;
using F12.Presentation;
using Microsoft.AspNetCore.Http;

namespace F12.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F11Endpoint";

    public const string ENDPOINT_PATH = "f12/task/{TodoTaskId:required}";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly AppResponseModel TODO_TASK_NOT_FOUND = new()
            {
                AppCode = AppCode.TODO_TASK_NOT_FOUND,
            };
        }

        public static class Http
        {
            public static readonly Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly Response SERVER_ERROR = new()
            {
                AppCode = (int)AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };

            public static readonly Response TODO_TASK_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.TODO_TASK_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        SERVER_ERROR,

        TODO_TASK_NOT_FOUND,
    }
}
