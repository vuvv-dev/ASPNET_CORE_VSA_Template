using F13.Presentation;
using Microsoft.AspNetCore.Http;

namespace F13.Common;

public static class F13Constant
{
    public const string ENDPOINT_PATH = "f13";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App { }

        public static class Http
        {
            public static readonly F13Response VALIDATION_FAILED = new()
            {
                AppCode = (int)AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };
        }
    }

    public enum AppCode
    {
        SUCCESS = 1,

        VALIDATION_FAILED,
    }
}
