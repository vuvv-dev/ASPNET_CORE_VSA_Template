using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class IdentityRoleClaimEntityConfiguration
    : IEntityTypeConfiguration<IdentityRoleClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
    {
        builder.ToTable(IdentityRoleClaimEntity.Metadata.TableName);
    }
}
