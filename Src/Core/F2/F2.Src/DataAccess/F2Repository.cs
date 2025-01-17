using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F2.Src.Models;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace F2.Src.DataAccess;

public sealed class F2Repository : IF2Repository
{
    private readonly AppDbContext _appContext;

    public F2Repository(AppDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<F2TodoTaskListModel> GetTodoTaskListAsync(long listId, CancellationToken ct)
    {
        TodoTaskListEntity foundTodoTaskList = null;

        await _appContext
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var transaction = await _appContext.Database.BeginTransactionAsync(
                    IsolationLevel.RepeatableRead,
                    ct
                );

                try
                {
                    foundTodoTaskList = await _appContext
                        .Set<TodoTaskListEntity>()
                        .AsNoTracking()
                        .Where(entity => entity.Id == listId)
                        .Select(entity => new TodoTaskListEntity { Name = entity.Name })
                        .FirstOrDefaultAsync(ct);
                    if (Equals(foundTodoTaskList, null))
                    {
                        foundTodoTaskList = null;

                        await transaction.CommitAsync(ct);
                    }
                    else
                    {
                        foundTodoTaskList.TodoTasks = await _appContext
                            .Set<TodoTaskEntity>()
                            .AsNoTracking()
                            .Where(entity => entity.TodoTaskListId == listId)
                            .Select(entity => new TodoTaskEntity
                            {
                                Id = entity.Id,
                                Content = entity.Content,
                                DueDate = entity.DueDate,
                                IsFinished = entity.IsFinished,
                                IsInMyDay = entity.IsInMyDay,
                                IsImportant = entity.IsImportant,
                            })
                            .OrderBy(entity => entity.Id)
                            .Skip(0)
                            .Take(5)
                            .ToListAsync(ct);

                        await transaction.CommitAsync(ct);
                    }
                }
                catch (DbUpdateException)
                {
                    await transaction.RollbackAsync(ct);
                }
            });

        if (Equals(foundTodoTaskList, null))
        {
            return null;
        }

        var todoTaskListModel = new F2TodoTaskListModel
        {
            Name = foundTodoTaskList.Name,
            TodoTasks = foundTodoTaskList.TodoTasks.Select(
                entity => new F2TodoTaskListModel.TodoTaskModel
                {
                    Id = entity.Id,
                    Name = entity.Content,
                    DueDate = entity.DueDate,
                    IsFinished = entity.IsFinished,
                    IsInMyDay = entity.IsInMyDay,
                    IsImportant = entity.IsImportant,
                }
            ),
        };

        return todoTaskListModel;
    }
}
