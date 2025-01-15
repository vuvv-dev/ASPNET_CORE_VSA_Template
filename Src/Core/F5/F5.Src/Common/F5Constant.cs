using F5.Src.Models;
using F5.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F5.Src.Common;

public static class F5Constant
{
    public const string ENDPOINT_PATH = "f5";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public const string APP_USER_PASSWORD_RESET_TOKEN_NAME = "AppUserPasswordResetToken";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F5AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };
        }

        public static class Http
        {
            public static readonly F5Response VALIDATION_FAILED = new()
            {
                AppCode = AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F5Response SERVER_ERROR = new()
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
