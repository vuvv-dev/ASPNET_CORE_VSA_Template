using F007.Models;
using F007.Presentation;
using Microsoft.AspNetCore.Http;

namespace F007.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F007Endpoint";

    public const string ENDPOINT_PATH = "F007";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly AppResponseModel LIST_ALREADY_EXISTS = new()
            {
                AppCode = AppCode.LIST_ALREADY_EXISTS,
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

            public static readonly Response LIST_ALREADY_EXISTS = new()
            {
                AppCode = (int)AppCode.LIST_ALREADY_EXISTS,
                HttpCode = StatusCodes.Status409Conflict,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        SERVER_ERROR,

        LIST_ALREADY_EXISTS,
    }
}
