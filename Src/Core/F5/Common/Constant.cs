using F5.Models;
using F5.Presentation;
using Microsoft.AspNetCore.Http;

namespace F5.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F5Endpoint";

    public const string ENDPOINT_PATH = "f5";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class APP_USER_REFRESH_TOKEN
    {
        public const string NAME = "AppUserRefreshToken";
    }

    public static class Exception
    {
        public static class Message
        {
            public const string BULK_OPERATION_FAILED = "Bulk operation failed.";
        }
    }

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly AppResponseModel TOKEN_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.TOKEN_DOES_NOT_EXIST,
            };

            public static readonly AppResponseModel PASSWORD_IS_INVALID = new()
            {
                AppCode = AppCode.PASSWORD_IS_INVALID,
            };
        }

        public static class Http
        {
            public static readonly Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly Response SERVER_ERROR = new()
            {
                AppCode = (int)AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };

            public static readonly Response TOKEN_DOES_NOT_EXIST = new()
            {
                AppCode = (int)AppCode.TOKEN_DOES_NOT_EXIST,
                HttpCode = StatusCodes.Status404NotFound,
            };

            public static readonly Response PASSWORD_IS_INVALID = new()
            {
                AppCode = (int)AppCode.PASSWORD_IS_INVALID,
                HttpCode = StatusCodes.Status422UnprocessableEntity,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        SERVER_ERROR,

        TOKEN_DOES_NOT_EXIST,

        PASSWORD_IS_INVALID,
    }
}
