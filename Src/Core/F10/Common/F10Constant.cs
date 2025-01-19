using F10.Presentation;
using Microsoft.AspNetCore.Http;

namespace F10.Common;

public static class F10Constant
{
    public const string ENDPOINT_PATH = "f10";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class UrlQuery
    {
        public const string ListId = "listId";

        public const string NumberOfRecord = "n";
    }

    public static class DefaultResponse
    {
        public static class App { }

        public static class Http
        {
            public static readonly F10Response VALIDATION_FAILED = new()
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
