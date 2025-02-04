using FA1.Common;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA1.EntityConfigurations;

public sealed class TodoTaskStepEntityConfiguration : IEntityTypeConfiguration<TodoTaskStepEntity>
{
    public void Configure(EntityTypeBuilder<TodoTaskStepEntity> builder)
    {
        builder.ToTable(TodoTaskStepEntity.Metadata.TableName);

        builder.HasKey(entity => entity.Id);

        builder
            .Property(entity => entity.Content)
            .HasColumnName(TodoTaskStepEntity.Metadata.Properties.Content.ColumnName)
            .HasColumnType(
                $"{Constant.DatabaseType.VARCHAR}({TodoTaskStepEntity.Metadata.Properties.Content.MaxLength})"
            )
            .IsRequired(TodoTaskStepEntity.Metadata.Properties.Content.IsNotNull);

        builder
            .Property(entity => entity.CreatedDate)
            .HasColumnName(TodoTaskStepEntity.Metadata.Properties.CreatedDate.ColumnName)
            .HasColumnType(Constant.DatabaseType.TIMESTAMPZ)
            .IsRequired(TodoTaskStepEntity.Metadata.Properties.CreatedDate.IsNotNull);

        builder
            .Property(entity => entity.TodoTaskId)
            .HasColumnName(TodoTaskStepEntity.Metadata.Properties.TodoTaskId.ColumnName)
            .HasColumnType(Constant.DatabaseType.LONG)
            .IsRequired(TodoTaskStepEntity.Metadata.Properties.TodoTaskId.IsNotNull);

        builder
            .HasOne(taskStep => taskStep.TodoTask)
            .WithMany(task => task.TodoTaskSteps)
            .HasForeignKey(taskStep => taskStep.TodoTaskId)
            .HasPrincipalKey(task => task.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
