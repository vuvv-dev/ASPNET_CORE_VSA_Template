using System;
using System.Threading;
using System.Threading.Tasks;
using F11.Models;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F11.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<bool> CreateTodoTaskAsync(TaskTodoModel todoTask, CancellationToken ct)
    {
        var newEntity = new TodoTaskEntity
        {
            Id = todoTask.Id,
            Content = todoTask.Content,
            CreatedDate = todoTask.CreatedDate,
            TodoTaskListId = todoTask.TodoTaskListId,
            DueDate = DateTime.MinValue.ToUniversalTime(),
            IsFinished = false,
            IsImportant = false,
            IsInMyDay = false,
            Note = string.Empty,
            RecurringExpression = string.Empty,
        };

        try
        {
            await _appContext.Set<TodoTaskEntity>().AddAsync(newEntity, ct);

            await _appContext.SaveChangesAsync(ct);

            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct)
    {
        return _appContext.Set<TodoTaskListEntity>().AnyAsync(entity => entity.Id == listId, ct);
    }
}
