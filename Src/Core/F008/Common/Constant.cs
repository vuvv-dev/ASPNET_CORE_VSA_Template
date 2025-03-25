using F008.Models;
using F008.Presentation;
using Microsoft.AspNetCore.Http;

namespace F008.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F008Endpoint";

    public const string ENDPOINT_PATH = "F008/list/{TodoTaskListId:required}";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly AppResponseModel LIST_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.LIST_DOES_NOT_EXIST,
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

            public static readonly Response LIST_DOES_NOT_EXIST = new()
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
