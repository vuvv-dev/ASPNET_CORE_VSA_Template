using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using Microsoft.EntityFrameworkCore;

namespace F008.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public Task<bool> DoesTaskTodoListExistAsync(long listId, CancellationToken ct)
    {
        return _appContext.Set<TodoTaskListEntity>().AnyAsync(entity => entity.Id == listId, ct);
    }

    public async Task<bool> RemoveTaskTodoListAsync(long listId, CancellationToken ct)
    {
        var dbResult = true;

        await _appContext
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var dbTransaction = await _appContext.Database.BeginTransactionAsync(
                    IsolationLevel.ReadCommitted,
                    ct
                );

                try
                {
                    // Remove list
                    await _appContext
                        .Set<TodoTaskListEntity>()
                        .Where(list => list.Id == listId)
                        .ExecuteDeleteAsync(ct);

                    // Remove all task steps
                    await _appContext
                        .Set<TodoTaskEntity>()
                        .Where(task => task.TodoTaskListId == listId)
                        .Select(task => task.Id)
                        .ForEachAsync(
                            async taskId =>
                            {
                                await _appContext
                                    .Set<TodoTaskStepEntity>()
                                    .Where(taskStep => taskStep.TodoTaskId == taskId)
                                    .ExecuteDeleteAsync(ct);
                            },
                            ct
                        );

                    // Remove all tasks
                    await _appContext
                        .Set<TodoTaskEntity>()
                        .Where(task => task.TodoTaskListId == listId)
                        .ExecuteDeleteAsync(ct);

                    await dbTransaction.CommitAsync(ct);
                }
                catch (DbUpdateException)
                {
                    await dbTransaction.RollbackAsync(ct);

                    dbResult = false;
                }
            });

        return dbResult;
    }
}
