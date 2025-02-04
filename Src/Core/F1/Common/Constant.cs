using F1.Models;
using F1.Presentation;
using Microsoft.AspNetCore.Http;

namespace F1.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F1Endpoint";

    public const string ENDPOINT_PATH = "f1";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class APP_USER_REFRESH_TOKEN
    {
        public const string NAME = "AppUserRefreshToken";

        public static class DURATION_IN_MINUTES
        {
            public const int REMEMBER_ME = 60 * 24 * 365;

            public const int NOT_REMEMBER_ME = 60 * 24 * 7;
        }
    }

    public static class APP_USER_ACCESS_TOKEN
    {
        public const int DURATION_IN_MINUTES = 60;
    }

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly AppResponseModel USER_NOT_FOUND = new()
            {
                AppCode = AppCode.USER_NOT_FOUND,
            };

            public static readonly AppResponseModel TEMPORARY_BANNED = new()
            {
                AppCode = AppCode.TEMPORARY_BANNED,
            };

            public static readonly AppResponseModel PASSWORD_IS_INCORRECT = new()
            {
                AppCode = AppCode.PASSWORD_IS_INCORRECT,
            };

            public static readonly AppResponseModel VALIDATION_FAILED = new()
            {
                AppCode = AppCode.VALIDATION_FAILED,
            };

            public static readonly AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };
        }

        public static class Http
        {
            public static readonly Response PASSWORD_IS_INCORRECT = new()
            {
                HttpCode = StatusCodes.Status401Unauthorized,
                AppCode = (int)AppCode.PASSWORD_IS_INCORRECT,
            };

            public static readonly Response TEMPORARY_BANNED = new()
            {
                HttpCode = StatusCodes.Status429TooManyRequests,
                AppCode = (int)AppCode.TEMPORARY_BANNED,
            };

            public static readonly Response USER_NOT_FOUND = new()
            {
                HttpCode = StatusCodes.Status404NotFound,
                AppCode = (int)AppCode.USER_NOT_FOUND,
            };

            public static readonly Response VALIDATION_FAILED = new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = (int)AppCode.VALIDATION_FAILED,
            };

            public static readonly Response SERVER_ERROR = new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = (int)AppCode.SERVER_ERROR,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        USER_NOT_FOUND,

        TEMPORARY_BANNED,

        PASSWORD_IS_INCORRECT,

        VALIDATION_FAILED,

        SERVER_ERROR,
    }
}
