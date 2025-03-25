using Base.FX001.Common;
using Base.FX001.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.FX001.EntityConfigurations;

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
