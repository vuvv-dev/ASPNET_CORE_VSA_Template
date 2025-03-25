using Base.FX001.Common;
using Base.FX001.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.FX001.EntityConfigurations;

public sealed class TodoTaskListEntityConfiguration : IEntityTypeConfiguration<TodoTaskListEntity>
{
    public void Configure(EntityTypeBuilder<TodoTaskListEntity> builder)
    {
        builder.ToTable(TodoTaskListEntity.Metadata.TableName);

        builder.HasKey(entity => entity.Id);

        builder
            .Property(entity => entity.Name)
            .HasColumnName(TodoTaskListEntity.Metadata.Properties.Name.ColumnName)
            .HasColumnType(
                $"{Constant.DatabaseType.VARCHAR}({TodoTaskListEntity.Metadata.Properties.Name.MaxLength})"
            )
            .IsRequired(TodoTaskListEntity.Metadata.Properties.Name.IsNotNull);

        builder
            .Property(entity => entity.CreatedDate)
            .HasColumnName(TodoTaskListEntity.Metadata.Properties.CreatedDate.ColumnName)
            .HasColumnType(Constant.DatabaseType.TIMESTAMPZ)
            .IsRequired(TodoTaskListEntity.Metadata.Properties.CreatedDate.IsNotNull);

        builder
            .Property(entity => entity.UserId)
            .HasColumnName(TodoTaskListEntity.Metadata.Properties.UserId.ColumnName)
            .HasColumnType(Constant.DatabaseType.LONG)
            .IsRequired(TodoTaskListEntity.Metadata.Properties.UserId.IsNotNull);

        builder
            .HasOne(taskList => taskList.User)
            .WithMany(user => user.TodoTaskLists)
            .HasForeignKey(taskList => taskList.UserId)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
