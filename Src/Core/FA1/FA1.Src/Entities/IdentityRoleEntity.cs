using Microsoft.AspNetCore.Identity;

namespace FA1.Src.Entities;

public sealed class IdentityRoleEntity : IdentityRole<long>
{
    public static class Metadata
    {
        public const string TableName = "role";
    }
}
