using FA1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.EntityConfigurations;

public sealed class BaseIdentityRoleClaimEntityConfiguration
    : IEntityTypeConfiguration<IdentityRoleClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
    {
        builder.ToTable(IdentityRoleClaimEntity.Metadata.TableName);
    }
}
