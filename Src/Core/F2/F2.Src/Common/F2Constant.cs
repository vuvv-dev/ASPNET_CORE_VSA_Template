using F2.Src.Presentation;

namespace F2.Src.Common;

public static class F2Constant
{
    public const string ENDPOINT_PATH = "f2/{Name:required:alpha}";

    public static readonly F2Response EMPTY_RESPONSE = new();

    public static class AppCode
    {
        public const int SUCCESS = 1;
    }
}
