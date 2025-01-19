using Microsoft.AspNetCore.Identity;

namespace FA1.Entities;

public sealed class IdentityUserClaimEntity : IdentityUserClaim<long>
{
    public static class Metadata
    {
        public const string TableName = "user_claim";
    }
}
