namespace F2.Src.Common;

public static class F2Constant
{
    public const string ENDPOINT_PATH = "f2/{ListId:required}";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class AppCode
    {
        public const int SUCCESS = 1;

        public const int LIST_NOT_FOUND = 2;

        public const int VALIDATION_FAILED = 3;
    }
}
