using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using Microsoft.EntityFrameworkCore;

namespace F012.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct)
    {
        return _appContext.Set<TodoTaskEntity>().AnyAsync(entity => entity.Id == taskId, ct);
    }

    public async Task<bool> RemoveTodoTaskAsync(long taskId, CancellationToken ct)
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
                    await _appContext
                        .Set<TodoTaskEntity>()
                        .Where(task => task.Id == taskId)
                        .ExecuteDeleteAsync(ct);
                    await _appContext
                        .Set<TodoTaskStepEntity>()
                        .Where(taskStep => taskStep.TodoTaskId == taskId)
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
