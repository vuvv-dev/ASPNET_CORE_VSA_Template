using F6.Src.Models;
using F6.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F6.Src.Common;

public static class F6Constant
{
    public const string ENDPOINT_PATH = "f6";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class APP_USER_ACCESS_TOKEN
    {
        public const int DURATION_IN_MINUTES = 60;
    }

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F6AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly F6AppResponseModel REFRESH_TOKEN_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.REFRESH_TOKEN_DOES_NOT_EXIST,
            };

            public static readonly F6AppResponseModel REFRESH_TOKEN_EXPIRED = new()
            {
                AppCode = AppCode.REFRESH_TOKEN_EXPIRED,
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

            public static readonly F6Response REFRESH_TOKEN_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.REFRESH_TOKEN_DOES_NOT_EXIST,
                HttpCode = StatusCodes.Status404NotFound,
            };

            public static readonly F6Response REFRESH_TOKEN_EXPIRED = new()
            {
                AppCode = AppCode.REFRESH_TOKEN_EXPIRED,
                HttpCode = StatusCodes.Status401Unauthorized,
            };
        }
    }

    public static class AppCode
    {
        public const int SUCCESS = 1;

        public const int VALIDATION_FAILED = 2;

        public const int SERVER_ERROR = 3;

        public const int REFRESH_TOKEN_DOES_NOT_EXIST = 4;

        public const int REFRESH_TOKEN_EXPIRED = 5;
    }
}
