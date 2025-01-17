using F2.Src.Models;
using F2.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F2.Src.Common;

public static class F2Constant
{
    public const string ENDPOINT_PATH = "f2/{TodoTaskListId:required}";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App
        {
            public static readonly F2AppResponseModel LIST_NOT_FOUND = new()
            {
                AppCode = AppCode.LIST_NOT_FOUND,
            };
        }

        public static class Http
        {
            public static readonly F2Response LIST_NOT_FOUND = new()
            {
                HttpCode = StatusCodes.Status404NotFound,
                AppCode = (int)AppCode.LIST_NOT_FOUND,
            };

            public static readonly F2Response VALIDATION_FAILED = new()
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
