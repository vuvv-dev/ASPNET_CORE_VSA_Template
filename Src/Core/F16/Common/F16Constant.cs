using F16.Models;
using F16.Presentation;
using Microsoft.AspNetCore.Http;

namespace F16.Common;

public static class F16Constant
{
    public const string ENDPOINT_PATH = "f16";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F16AppResponseModel TASK_NOT_FOUND = new()
            {
                AppCode = AppCode.TASK_NOT_FOUND,
            };

            public static readonly F16AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };
        }

        public static class Http
        {
            public static readonly F16Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F16Response TASK_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.TASK_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
            };

            public static readonly F16Response SERVER_ERROR = new()
            {
                AppCode = (int)AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        TASK_NOT_FOUND,

        SERVER_ERROR,
    }
}
