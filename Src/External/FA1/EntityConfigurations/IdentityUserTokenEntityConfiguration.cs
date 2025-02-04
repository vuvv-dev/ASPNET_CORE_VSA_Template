using FA1.Common;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.EntityConfigurations;

public sealed class IdentityUserTokenEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserTokenEntity>
{
    public void Configure(EntityTypeBuilder<IdentityUserTokenEntity> builder)
    {
        builder
            .Property(entity => entity.ExpiredAt)
            .HasColumnName(IdentityUserTokenEntity.Metadata.Properties.ExpiredAt.ColumnName)
            .HasColumnType(Constant.DatabaseType.TIMESTAMPZ)
            .IsRequired(IdentityUserTokenEntity.Metadata.Properties.ExpiredAt.IsNotNull);
    }
}
