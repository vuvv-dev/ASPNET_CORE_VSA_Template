using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class IdentityUserRoleEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserRoleEntity>
{
    public void Configure(EntityTypeBuilder<IdentityUserRoleEntity> builder)
    {
        builder.ToTable(IdentityUserRoleEntity.Metadata.TableName);
    }
}
