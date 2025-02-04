using FA1.Common;
using FA1.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FA1.DbContext;

public sealed class AppDbContext : IdentityDbContext<IdentityUserEntity, IdentityRoleEntity, long>
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.HasDefaultSchema(Constant.DatabaseSchema);

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
            Constant.Collation.CASE_INSENSITIVE,
            "en-u-ks-primary",
            "icu",
            false
        );
    }
}
