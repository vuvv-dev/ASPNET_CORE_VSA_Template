using FA1.Src.Common;
using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FA1.Src;

public sealed class AppContext : IdentityDbContext<IdentityUserEntity, IdentityRoleEntity, long>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

        builder.HasDefaultSchema(FA1Constant.DatabaseSchema);
    }
}