using F4.Models;
using F4.Presentation;
using Microsoft.AspNetCore.Http;

namespace F4.Common;

public static class F4Constant
{
    public const string ENDPOINT_PATH = "f4";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class APP_USER_PASSWORD_RESET_TOKEN
    {
        public const string NAME = "AppUserPasswordResetToken";

        public const int DURATION_IN_MINUTES = 15;
    }

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
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F4Response USER_NOT_FOUND = new()
            {
                AppCode = (int)AppCode.USER_NOT_FOUND,
                HttpCode = StatusCodes.Status404NotFound,
            };

            public static readonly F4Response SERVER_ERROR = new()
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

        SERVER_ERROR,

        USER_NOT_FOUND,
    }
}
