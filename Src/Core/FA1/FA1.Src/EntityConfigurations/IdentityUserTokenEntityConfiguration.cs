using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class IdentityUserTokenEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserTokenEntity>
{
    public void Configure(EntityTypeBuilder<IdentityUserTokenEntity> builder)
    {
        builder.ToTable(IdentityUserTokenEntity.Metadata.TableName);
    }
}
