using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class IdentityRoleClaimEntityConfiguration
    : IEntityTypeConfiguration<IdentityRoleClaimEntity>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaimEntity> builder)
    {
        builder.ToTable(IdentityRoleClaimEntity.Metadata.TableName);
    }
}
