using F4.Src.Models;
using F4.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F4.Src.Common;

public static class F4Constant
{
    public const string ENDPOINT_PATH = "f4";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public const string APP_USER_PASSWORD_RESET_TOKEN_NAME = "AppUserPasswordResetToken";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F4AppResponseModel USER_NOT_FOUND = new()
            {
                AppCode = AppCode.USER_NOT_FOUND,
            };

            public static readonly F4AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };
        }

        public static class Http
        {
            public static readonly F4Response VALIDATION_FAILED = new()
            {
                AppCode = AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F4Response USER_NOT_FOUND = new()
            {
                AppCode = AppCode.USER_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
            };

            public static readonly F4Response SERVER_ERROR = new()
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

        public const int USER_NOT_FOUND = 4;
    }
}
