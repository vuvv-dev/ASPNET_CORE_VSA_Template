using F1.Src.Models;
using F1.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F1.Src.Common;

public static class F1Constant
{
    public const string ENDPOINT_PATH = "f1";

    public const string APP_USER_REFRESH_TOKEN_NAME = "AppUserRefreshToken";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F1AppResponseModel USER_NOT_FOUND = new()
            {
                AppCode = AppCode.USER_NOT_FOUND,
            };

            public static readonly F1AppResponseModel TEMPORARY_BANNED = new()
            {
                AppCode = AppCode.TEMPORARY_BANNED,
            };

            public static readonly F1AppResponseModel PASSWORD_IS_INCORRECT = new()
            {
                AppCode = AppCode.PASSWORD_IS_INCORRECT,
            };

            public static readonly F1AppResponseModel VALIDATION_FAILED = new()
            {
                AppCode = AppCode.VALIDATION_FAILED,
            };

            public static readonly F1AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };
        }

        public static class Http
        {
            public static readonly F1Response PASSWORD_IS_INCORRECT = new()
            {
                HttpCode = StatusCodes.Status401Unauthorized,
                AppCode = AppCode.PASSWORD_IS_INCORRECT,
            };

            public static readonly F1Response TEMPORARY_BANNED = new()
            {
                HttpCode = StatusCodes.Status429TooManyRequests,
                AppCode = AppCode.TEMPORARY_BANNED,
            };

            public static readonly F1Response USER_NOT_FOUND = new()
            {
                HttpCode = StatusCodes.Status404NotFound,
                AppCode = AppCode.USER_NOT_FOUND,
            };

            public static readonly F1Response VALIDATION_FAILED = new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = AppCode.VALIDATION_FAILED,
            };

            public static readonly F1Response SERVER_ERROR = new()
            {
                HttpCode = StatusCodes.Status500InternalServerError,
                AppCode = AppCode.SERVER_ERROR,
            };
        }
    }

    public static class AppCode
    {
        public const int SUCCESS = 1;

        public const int USER_NOT_FOUND = 2;

        public const int TEMPORARY_BANNED = 3;

        public const int PASSWORD_IS_INCORRECT = 4;

        public const int VALIDATION_FAILED = 5;

        public const int SERVER_ERROR = 6;
    }
}
