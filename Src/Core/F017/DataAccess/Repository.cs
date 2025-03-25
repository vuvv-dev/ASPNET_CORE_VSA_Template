using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using Microsoft.EntityFrameworkCore;

namespace F017.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<bool> ModifyCompletedStatusAsync(
        long taskId,
        bool isCompleted,
        CancellationToken ct
    )
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
                        .ExecuteUpdateAsync(
                            setProp =>
                                setProp.SetProperty(entity => entity.IsFinished, isCompleted),
                            ct
                        );

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

    public Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct)
    {
        return _appContext.Set<TodoTaskEntity>().AnyAsync(entity => entity.Id == taskId, ct);
    }
}
