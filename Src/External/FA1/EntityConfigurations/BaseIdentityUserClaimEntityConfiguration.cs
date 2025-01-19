using FA1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.EntityConfigurations;

public sealed class BaseIdentityUserClaimEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
    {
        builder.ToTable(IdentityUserClaimEntity.Metadata.TableName);
    }
}
