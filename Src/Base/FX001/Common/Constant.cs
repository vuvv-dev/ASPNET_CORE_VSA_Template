namespace Base.FX001.Common;

internal static class Constant
{
    public const string DatabaseSchema = "todo";

    public static class Collation
    {
        public const string CASE_INSENSITIVE = "case_insensitive";
    }

    public static class DatabaseType
    {
        public const string VARCHAR = "VARCHAR";

        public const string LONG = "BIGINT";

        public const string TIMESTAMPZ = "TIMESTAMP WITH TIME ZONE";
    }
}
