using FA1.Src.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

public sealed class BaseIdentityUserTokenEntityConfiguration
    : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
    {
        builder.ToTable(IdentityUserTokenEntity.Metadata.TableName);
    }
}
