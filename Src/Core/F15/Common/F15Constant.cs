using F15.Models;
using F15.Presentation;
using Microsoft.AspNetCore.Http;

namespace F15.Common;

public static class F15Constant
{
    public const string ENDPOINT_PATH = "f15/task/{TodoTaskId:required}";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F15AppResponseModel TASK_NOT_FOUND = new()
            {
                AppCode = AppCode.TASK_NOT_FOUND,
            };
        }

        public static class Http
        {
            public static readonly F15Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F15Response TASK_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.TASK_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        TASK_NOT_FOUND,
    }
}
