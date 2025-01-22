using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F20.DataAccess;

public sealed class F20Repository : IF20Repository
{
    private readonly AppDbContext _appContext;

    public F20Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<bool> ChangeNoteAsync(long taskId, string note, CancellationToken ct)
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
                            setProp => setProp.SetProperty(entity => entity.Note, note),
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
