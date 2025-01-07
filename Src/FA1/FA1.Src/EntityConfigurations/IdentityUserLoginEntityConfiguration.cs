using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class IdentityUserLoginEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserLoginEntity>
{
    public void Configure(EntityTypeBuilder<IdentityUserLoginEntity> builder)
    {
        builder.ToTable(IdentityUserLoginEntity.Metadata.TableName);
    }
}
