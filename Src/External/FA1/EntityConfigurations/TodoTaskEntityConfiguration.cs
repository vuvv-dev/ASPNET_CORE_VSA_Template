using FA1.Common;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.EntityConfigurations;

public sealed class TodoTaskEntityConfiguration : IEntityTypeConfiguration<TodoTaskEntity>
{
    public void Configure(EntityTypeBuilder<TodoTaskEntity> builder)
    {
        builder.ToTable(TodoTaskEntity.Metadata.TableName);

        builder.HasKey(entity => entity.Id);

        builder
            .Property(entity => entity.Content)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.Content.ColumnName)
            .HasColumnType(
                $"{FA1Constant.DatabaseType.VARCHAR}({TodoTaskEntity.Metadata.Properties.Content.MaxLength})"
            )
            .IsRequired(TodoTaskEntity.Metadata.Properties.Content.IsNotNull);

        builder
            .Property(entity => entity.Note)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.Note.ColumnName)
            .HasColumnType(
                $"{FA1Constant.DatabaseType.VARCHAR}({TodoTaskEntity.Metadata.Properties.Note.MaxLength})"
            )
            .IsRequired(TodoTaskEntity.Metadata.Properties.Note.IsNotNull);

        builder
            .Property(entity => entity.CreatedDate)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.CreatedDate.ColumnName)
            .HasColumnType(FA1Constant.DatabaseType.TIMESTAMPZ)
            .IsRequired(TodoTaskEntity.Metadata.Properties.CreatedDate.IsNotNull);

        builder
            .Property(entity => entity.DueDate)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.DueDate.ColumnName)
            .HasColumnType(FA1Constant.DatabaseType.TIMESTAMPZ)
            .IsRequired(TodoTaskEntity.Metadata.Properties.DueDate.IsNotNull);

        builder
            .Property(entity => entity.IsInMyDay)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.IsInMyDay.ColumnName)
            .IsRequired(TodoTaskEntity.Metadata.Properties.IsInMyDay.IsNotNull);

        builder
            .Property(entity => entity.IsImportant)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.IsImportant.ColumnName)
            .IsRequired(TodoTaskEntity.Metadata.Properties.IsImportant.IsNotNull);

        builder
            .Property(entity => entity.IsFinished)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.IsFinished.ColumnName)
            .IsRequired(TodoTaskEntity.Metadata.Properties.IsFinished.IsNotNull);

        builder
            .Property(entity => entity.RecurringExpression)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.RecurringExpression.ColumnName)
            .HasColumnType(
                $"{FA1Constant.DatabaseType.VARCHAR}({TodoTaskEntity.Metadata.Properties.RecurringExpression.MaxLength})"
            )
            .IsRequired(TodoTaskEntity.Metadata.Properties.RecurringExpression.IsNotNull);

        builder
            .Property(entity => entity.TodoTaskListId)
            .HasColumnName(TodoTaskEntity.Metadata.Properties.TodoTaskListId.ColumnName)
            .HasColumnType(FA1Constant.DatabaseType.LONG)
            .IsRequired(TodoTaskEntity.Metadata.Properties.TodoTaskListId.IsNotNull);

        builder
            .HasOne(task => task.TodoTaskList)
            .WithMany(taskList => taskList.TodoTasks)
            .HasForeignKey(task => task.TodoTaskListId)
            .HasPrincipalKey(taskList => taskList.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
