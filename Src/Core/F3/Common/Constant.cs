using F3.Models;
using F3.Presentation;
using Microsoft.AspNetCore.Http;

namespace F3.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F3Endpoint";

    public const string ENDPOINT_PATH = "f3";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly AppResponseModel EMAIL_ALREADY_EXISTS = new()
            {
                AppCode = AppCode.EMAIL_ALREADY_EXISTS,
            };

            public static readonly AppResponseModel PASSWORD_IS_INVALID = new()
            {
                AppCode = AppCode.PASSWORD_IS_INVALID,
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
            public static readonly Response PASSWORD_IS_INVALID = new()
            {
                HttpCode = StatusCodes.Status422UnprocessableEntity,
                AppCode = (int)AppCode.PASSWORD_IS_INVALID,
            };

            public static readonly Response EMAIL_ALREADY_EXISTS = new()
            {
                HttpCode = StatusCodes.Status409Conflict,
                AppCode = (int)AppCode.EMAIL_ALREADY_EXISTS,
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

        EMAIL_ALREADY_EXISTS,

        PASSWORD_IS_INVALID,

        VALIDATION_FAILED,

        SERVER_ERROR,
    }
}
