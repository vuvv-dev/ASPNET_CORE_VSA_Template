using Microsoft.AspNetCore.Identity;

namespace FA1.Entities;

public sealed class IdentityRoleClaimEntity : IdentityRoleClaim<long>
{
    public static class Metadata
    {
        public const string TableName = "role_claim";
    }
}
