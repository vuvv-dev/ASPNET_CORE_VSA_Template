using F6.Models;
using F6.Presentation;
using Microsoft.AspNetCore.Http;

namespace F6.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F6Endpoint";
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
            public static readonly AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly AppResponseModel REFRESH_TOKEN_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.REFRESH_TOKEN_DOES_NOT_EXIST,
            };

            public static readonly AppResponseModel REFRESH_TOKEN_EXPIRED = new()
            {
                AppCode = AppCode.REFRESH_TOKEN_EXPIRED,
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

            public static readonly Response REFRESH_TOKEN_DOES_NOT_EXIST = new()
            {
                AppCode = (int)AppCode.REFRESH_TOKEN_DOES_NOT_EXIST,
                HttpCode = StatusCodes.Status404NotFound,
            };

            public static readonly Response REFRESH_TOKEN_EXPIRED = new()
            {
                AppCode = (int)AppCode.REFRESH_TOKEN_EXPIRED,
                HttpCode = StatusCodes.Status401Unauthorized,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        SERVER_ERROR,

        REFRESH_TOKEN_DOES_NOT_EXIST,

        REFRESH_TOKEN_EXPIRED,
    }
}
