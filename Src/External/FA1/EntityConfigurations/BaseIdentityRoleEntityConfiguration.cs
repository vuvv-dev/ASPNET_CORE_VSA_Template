using FA1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.EntityConfigurations;

public sealed class BaseIdentityRoleEntityConfiguration
    : IEntityTypeConfiguration<IdentityRoleEntity>
{
    public void Configure(EntityTypeBuilder<IdentityRoleEntity> builder)
    {
        builder.ToTable(IdentityRoleEntity.Metadata.TableName);
    }
}
