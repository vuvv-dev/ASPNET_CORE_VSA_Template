using FA1.Common;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.EntityConfigurations;

public sealed class AdditionalUserInfoEntityConfiguration
    : IEntityTypeConfiguration<AdditionalUserInfoEntity>
{
    public void Configure(EntityTypeBuilder<AdditionalUserInfoEntity> builder)
    {
        builder.ToTable(AdditionalUserInfoEntity.Metadata.TableName);

        builder.HasKey(entity => entity.Id);

        builder
            .Property(entity => entity.FirstName)
            .HasColumnName(AdditionalUserInfoEntity.Metadata.Properties.FirstName.ColumnName)
            .HasColumnType(
                $"{FA1Constant.DatabaseType.VARCHAR}({AdditionalUserInfoEntity.Metadata.Properties.FirstName.MaxLength})"
            )
            .IsRequired(AdditionalUserInfoEntity.Metadata.Properties.FirstName.IsNotNull);

        builder
            .Property(entity => entity.LastName)
            .HasColumnName(AdditionalUserInfoEntity.Metadata.Properties.LastName.ColumnName)
            .HasColumnType(
                $"{FA1Constant.DatabaseType.VARCHAR}({AdditionalUserInfoEntity.Metadata.Properties.LastName.MaxLength})"
            )
            .IsRequired(AdditionalUserInfoEntity.Metadata.Properties.LastName.IsNotNull);

        builder
            .Property(entity => entity.Description)
            .HasColumnName(AdditionalUserInfoEntity.Metadata.Properties.Description.ColumnName)
            .HasColumnType(
                $"{FA1Constant.DatabaseType.VARCHAR}({AdditionalUserInfoEntity.Metadata.Properties.Description.MaxLength})"
            )
            .IsRequired(AdditionalUserInfoEntity.Metadata.Properties.Description.IsNotNull);
    }
}
