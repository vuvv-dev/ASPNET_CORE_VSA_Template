using System.IdentityModel.Tokens.Jwt;

namespace FCommon.Src.Constants;

public static class AppConstants
{
    public static class JsonWebToken
    {
        public static class Type
        {
            public const string JWE = "JWE";

            public const string JWS = "JWS";
        }

        public static class ClaimType
        {
            public const string JTI = JwtRegisteredClaimNames.Jti;

            public const string EXP = JwtRegisteredClaimNames.Exp;

            public const string SUB = JwtRegisteredClaimNames.Sub;

            public const string RES_PASS = "res_pass";
        }
    }
}
