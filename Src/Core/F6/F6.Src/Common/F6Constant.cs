using F6.Src.Models;
using F6.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F6.Src.Common;

public static class F6Constant
{
    public const string ENDPOINT_PATH = "f6";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F6AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly F6AppResponseModel TOKEN_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.TOKEN_DOES_NOT_EXIST,
            };
        }

        public static class Http
        {
            public static readonly F6Response VALIDATION_FAILED = new()
            {
                AppCode = AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F6Response SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };

            public static readonly F6Response TOKEN_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.TOKEN_DOES_NOT_EXIST,
                HttpCode = StatusCodes.Status404NotFound,
            };
        }
    }

    public static class AppCode
    {
        public const int SUCCESS = 1;

        public const int VALIDATION_FAILED = 2;

        public const int SERVER_ERROR = 3;

        public const int TOKEN_DOES_NOT_EXIST = 4;
    }
}
