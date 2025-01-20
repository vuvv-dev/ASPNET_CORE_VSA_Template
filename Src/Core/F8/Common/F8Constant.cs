using F8.Models;
using F8.Presentation;
using Microsoft.AspNetCore.Http;

namespace F8.Common;

public static class F8Constant
{
    public const string ENDPOINT_PATH = "f8/list/{TodoTaskListId:required}";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F8AppResponseModel SERVER_ERROR = new()
            {
                AppCode = AppCode.SERVER_ERROR,
            };

            public static readonly F8AppResponseModel LIST_DOES_NOT_EXIST = new()
            {
                AppCode = AppCode.LIST_DOES_NOT_EXIST,
            };
        }

        public static class Http
        {
            public static readonly F8Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };

            public static readonly F8Response SERVER_ERROR = new()
            {
                AppCode = (int)AppCode.SERVER_ERROR,
                HttpCode = StatusCodes.Status500InternalServerError,
            };

            public static readonly F8Response LIST_DOES_NOT_EXIST = new()
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
