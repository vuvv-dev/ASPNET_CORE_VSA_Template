using FA1.Src.Common;
using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class IdentityUserTokenEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserTokenEntity>
{
    public void Configure(EntityTypeBuilder<IdentityUserTokenEntity> builder)
    {
        builder
            .Property(entity => entity.ExpiredAt)
            .HasColumnName(IdentityUserTokenEntity.Metadata.Properties.ExpiredAt.ColumnName)
            .HasColumnType(FA1Constant.DatabaseType.TIMESTAMPZ)
            .IsRequired(IdentityUserTokenEntity.Metadata.Properties.ExpiredAt.IsNotNull);
    }
}
