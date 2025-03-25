using Microsoft.AspNetCore.Identity;

namespace Base.FX001.Entities;

public sealed class IdentityRoleEntity : IdentityRole<long>
{
    public static class Metadata
    {
        public const string TableName = "role";
    }
}
