using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class IdentityUserEntityConfiguration : IEntityTypeConfiguration<IdentityUserEntity>
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
