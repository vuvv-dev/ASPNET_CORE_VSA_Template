using FA1.Src.Common;
using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.Src.EntityConfigurations;

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
                $"{FA1Constant.DatabaseType.VARCHAR}({TodoTaskListEntity.Metadata.Properties.Name.MaxLength})"
            )
            .IsRequired(TodoTaskListEntity.Metadata.Properties.Name.IsNotNull);

        builder
            .Property(entity => entity.CreatedDate)
            .HasColumnName(TodoTaskListEntity.Metadata.Properties.CreatedDate.ColumnName)
            .HasColumnType(FA1Constant.DatabaseType.TIMESTAMPZ)
            .IsRequired(TodoTaskListEntity.Metadata.Properties.CreatedDate.IsNotNull);

        builder
            .Property(entity => entity.UserId)
            .HasColumnName(TodoTaskListEntity.Metadata.Properties.UserId.ColumnName)
            .HasColumnType(FA1Constant.DatabaseType.LONG)
            .IsRequired(TodoTaskListEntity.Metadata.Properties.UserId.IsNotNull);

        builder
            .HasOne(taskList => taskList.User)
            .WithMany(user => user.TodoTaskLists)
            .HasForeignKey(taskList => taskList.UserId)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
