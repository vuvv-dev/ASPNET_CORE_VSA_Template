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
        }

        public static class Http
        {
            public static readonly F7Response VALIDATION_FAILED = new()
            {
                AppCode = AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F7Response SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };
        }
    }

    public static class AppCode
    {
        public const int SUCCESS = 1;

        public const int VALIDATION_FAILED = 2;

        public const int SERVER_ERROR = 3;
    }
}
