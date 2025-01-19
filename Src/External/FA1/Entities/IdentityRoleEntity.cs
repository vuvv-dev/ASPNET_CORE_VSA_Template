using Microsoft.AspNetCore.Identity;

namespace FA1.Entities;

public sealed class IdentityRoleEntity : IdentityRole<long>
{
    public static class Metadata
    {
        public const string TableName = "role";
    }
}
