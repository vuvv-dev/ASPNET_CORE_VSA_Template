using Microsoft.AspNetCore.Identity;

namespace Base.FX001.Entities;

public sealed class IdentityUserLoginEntity : IdentityUserLogin<long>
{
    public static class Metadata
    {
        public const string TableName = "user_login";
    }
}
