using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class IdentityUserClaimEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserClaimEntity>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaimEntity> builder)
    {
        builder.ToTable(IdentityUserClaimEntity.Metadata.TableName);
    }
}
