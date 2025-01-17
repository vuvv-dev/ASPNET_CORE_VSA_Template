using F7.Src.Models;
using F7.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F7.Src.Common;

public static class F7Constant
{
    public const string ENDPOINT_PATH = "f7";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F7AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly F7AppResponseModel LIST_ALREADY_EXISTS = new()
            {
                AppCode = AppCode.LIST_ALREADY_EXISTS,
            };
        }

        public static class Http
        {
            public static readonly F7Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F7Response SERVER_ERROR = new()
            {
                AppCode = (int)AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };

            public static readonly F7Response LIST_ALREADY_EXISTS = new()
            {
                AppCode = (int)AppCode.LIST_ALREADY_EXISTS,
                HttpCode = StatusCodes.Status409Conflict,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        SERVER_ERROR,

        LIST_ALREADY_EXISTS,
    }
}
