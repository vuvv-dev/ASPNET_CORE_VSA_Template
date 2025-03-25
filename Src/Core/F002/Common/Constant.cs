using F002.Models;
using F002.Presentation;
using Microsoft.AspNetCore.Http;

namespace F002.Common;

public static class Constant
{
    public const string CONTROLLER_NAME = "F002Endpoint";

    public const string ENDPOINT_PATH = "F002/list/{TodoTaskListId:required}";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly AppResponseModel LIST_NOT_FOUND = new()
            {
                AppCode = AppCode.LIST_NOT_FOUND,
            };
        }

        public static class Http
        {
            public static readonly Response LIST_NOT_FOUND = new()
            {
                HttpCode = StatusCodes.Status404NotFound,
                AppCode = (int)AppCode.LIST_NOT_FOUND,
            };

            public static readonly Response VALIDATION_FAILED = new()
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = (int)AppCode.VALIDATION_FAILED,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        LIST_NOT_FOUND,

        VALIDATION_FAILED,
    }
}
