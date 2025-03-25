using Microsoft.AspNetCore.Identity;

namespace Base.FX001.Entities;

public sealed class IdentityRoleClaimEntity : IdentityRoleClaim<long>
{
    public static class Metadata
    {
        public const string TableName = "role_claim";
    }
}
