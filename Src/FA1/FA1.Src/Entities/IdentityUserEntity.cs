using Microsoft.AspNetCore.Identity;

namespace FA1.Src.Entities;

public sealed class IdentityUserEntity : IdentityUser<long> 
{
    public static class Metadata
    {
        public const string TableName = "user";
    }
}
