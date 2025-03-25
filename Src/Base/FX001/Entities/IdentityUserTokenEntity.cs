using System;
using Microsoft.AspNetCore.Identity;

namespace Base.FX001.Entities;

public sealed class IdentityUserTokenEntity : IdentityUserToken<long>
{
    public DateTime ExpiredAt { get; set; }

    public static class Metadata
    {
        public const string TableName = "user_token";

        public static class Properties
        {
            public static class ExpiredAt
            {
                public const string ColumnName = "expired_at";

                public const bool IsNotNull = true;
            }
        }
    }
}
