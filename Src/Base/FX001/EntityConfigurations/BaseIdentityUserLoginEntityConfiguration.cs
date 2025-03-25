using Base.FX001.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.FX001.EntityConfigurations;

public sealed class BaseIdentityUserLoginEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
    {
        builder.ToTable(IdentityUserLoginEntity.Metadata.TableName);
    }
}
