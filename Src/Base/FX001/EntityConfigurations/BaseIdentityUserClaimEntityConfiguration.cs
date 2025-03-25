using Base.FX001.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.FX001.EntityConfigurations;

public sealed class BaseIdentityUserClaimEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
    {
        builder.ToTable(IdentityUserClaimEntity.Metadata.TableName);
    }
}
