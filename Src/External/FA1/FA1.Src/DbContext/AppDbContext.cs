using FA1.Src.Common;
using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FA1.Src.DbContext;

public sealed class AppDbContext : IdentityDbContext<IdentityUserEntity, IdentityRoleEntity, long>
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

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
        builder.HasCollation(
            FA1Constant.Collation.CASE_INSENSITIVE,
            "en-u-ks-primary",
            "icu",
            false
        );
    }
}
