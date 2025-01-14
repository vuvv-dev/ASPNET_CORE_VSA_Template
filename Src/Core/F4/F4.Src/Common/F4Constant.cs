using F4.Src.Presentation;
using Microsoft.AspNetCore.Http;

namespace F4.Src.Common;

public static class F4Constant
{
    public const string ENDPOINT_PATH = "f4";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class DefaultResponse
    {
        public static class App { }

        public static class Http
        {
            public static readonly F4Response VALIDATION_FAILED = new()
            {
                AppCode = AppCode.VALIDATION_FAILED,
                HttpCode = StatusCodes.Status400BadRequest,
            };
        }
    }

    public static class AppCode
    {
        public const int SUCCESS = 1;

        public const int VALIDATION_FAILED = 2;

        public const int SERVER_ERROR = 3;
    }
}
