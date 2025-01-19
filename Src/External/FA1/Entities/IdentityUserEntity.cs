using Microsoft.AspNetCore.Identity;

namespace FA1.Entities;

public sealed class IdentityUserEntity : IdentityUser<long>
{
    #region Navigations
    public AdditionalUserInfoEntity AdditionalUserInfo { get; set; }
    #endregion

    public static class Metadata
    {
        public const string TableName = "user";
    }
}
