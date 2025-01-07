using FA1.Src.Common;
using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FA1.Src.DbContext;

public sealed class AppContext : IdentityDbContext<IdentityUserEntity, IdentityRoleEntity, long>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

        builder.HasDefaultSchema(FA1Constant.DatabaseSchema);

        InitCaseInsensitiveCollation(builder);
    }

    /// <summary>
    ///     Create new case insensitive collation.
    /// </summary>
    /// <param name="builder">
    ///     Model builder access the database.
    /// </param>
    private static void InitCaseInsensitiveCollation(ModelBuilder builder)
    {
        builder.HasCollation("case_insensitive", "en-u-ks-primary", "icu", false);
    }
}
