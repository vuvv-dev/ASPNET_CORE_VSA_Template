using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using F014.Models;
using Microsoft.EntityFrameworkCore;

namespace F014.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public Task<bool> DoesTodoTaskExistAsync(long taskId, long listId, CancellationToken ct)
    {
        return _appContext
            .Set<TodoTaskEntity>()
            .AnyAsync(entity => entity.Id == taskId && entity.TodoTaskListId == listId, ct);
    }

    public Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct)
    {
        return _appContext.Set<TodoTaskListEntity>().AnyAsync(entity => entity.Id == listId, ct);
    }

    public async Task<IEnumerable<TodoTaskModel>> GetCompletedTodoTasksAsync(
        GetTodoTasksInputModel input,
        CancellationToken ct
    )
    {
        return await _appContext
            .Set<TodoTaskEntity>()
            .Where(entity =>
                entity.Id >= input.TodoTaskId
                && entity.TodoTaskListId == input.TodoTaskListId
                && entity.IsFinished == true
            )
            .Select(entity => new TodoTaskModel
            {
                Id = entity.Id,
                Content = entity.Content,
                DueDate = entity.DueDate,
                IsImportant = entity.IsImportant,
                IsInMyDay = entity.IsInMyDay,
                HasNote = !string.IsNullOrEmpty(entity.Note),
                IsRecurring = !string.IsNullOrEmpty(entity.RecurringExpression),
                HasSteps = entity.TodoTaskSteps.Any(),
            })
            .OrderBy(entity => entity.Id)
            .ThenBy(entity => entity.IsImportant)
            .Take(input.NumberOfRecord + 1)
            .ToListAsync(ct);
    }
}
