using Microsoft.AspNetCore.Identity;

namespace FA1.Src.Entities;

public sealed class IdentityUserTokenEntity : IdentityUserToken<long>
{
    public static class Metadata
    {
        public const string TableName = "user_token";
    }
}
