using System.IdentityModel.Tokens.Jwt;

namespace FCommon.Src.Constants;

public static class AppConstants
{
    public const string STATE_BAG_NAME = "StateBag";

    public const int REFRESH_TOKEN_LENGTH = 15;

    public static class JsonWebToken
    {
        public static class Type
        {
            public const string JWT = "JWT";
        }

        public static class ClaimType
        {
            public const string JTI = JwtRegisteredClaimNames.Jti;

            public const string EXP = JwtRegisteredClaimNames.Exp;

            public const string SUB = JwtRegisteredClaimNames.Sub;

            public static class PURPOSE
            {
                public const string Name = "purpose";

                public static class Value
                {
                    public const string USER_PASSWORD_RESET = "user_password_reset";

                    public const string USER_IN_APP = "user_in_app";
                }
            }
        }
    }
}
