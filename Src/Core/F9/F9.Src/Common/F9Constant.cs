using F9.Src.Models;
using F9.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F9.Src.Common;

public static class F9Constant
{
    public const string ENDPOINT_PATH = "f9";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F9AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly F9AppResponseModel LIST_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.LIST_DOES_NOT_EXIST,
            };
        }

        public static class Http
        {
            public static readonly F9Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F9Response SERVER_ERROR = new()
            {
                AppCode = (int)AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };

            public static readonly F9Response LIST_DOES_NOT_EXIST = new()
            {
                AppCode = (int)AppCode.LIST_DOES_NOT_EXIST,
                HttpCode = StatusCodes.Status404NotFound,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,

        SERVER_ERROR,

        LIST_DOES_NOT_EXIST,
    }
}
