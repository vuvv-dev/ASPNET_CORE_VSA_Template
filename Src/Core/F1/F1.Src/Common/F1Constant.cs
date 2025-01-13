namespace F1.Src.Common;

public static class F1Constant
{
    public const string ENDPOINT_PATH = "f1/{ListId:required}";

    public const string APP_USER_REFRESH_TOKEN_NAME = "AppUserRefreshToken";

    public const string REQUEST_ARGUMENT_NAME = "request";

    public static class AppCode
    {
        public const int SUCCESS = 1;

        public const int USER_NOT_FOUND = 2;

        public const int TEMPORARY_BANNED = 3;

        public const int PASSWORD_IS_INCORRECT = 4;

        public const int VALIDATION_FAILED = 5;
    }
}
