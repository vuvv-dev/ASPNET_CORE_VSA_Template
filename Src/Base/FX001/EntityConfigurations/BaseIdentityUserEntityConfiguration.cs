using Base.FX001.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.FX001.EntityConfigurations;

public sealed class BaseIdentityUserEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserEntity>
{
    public void Configure(EntityTypeBuilder<IdentityUserEntity> builder)
    {
        builder.ToTable(IdentityUserEntity.Metadata.TableName);

        builder
            .HasOne(user => user.AdditionalUserInfo)
            .WithOne(additionalUserInfo => additionalUserInfo.IdentityUser)
            .HasForeignKey<AdditionalUserInfoEntity>(additionalUserInfo => additionalUserInfo.Id)
            .HasPrincipalKey<IdentityUserEntity>(user => user.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
