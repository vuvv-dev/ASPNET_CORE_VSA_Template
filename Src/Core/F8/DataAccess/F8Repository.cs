using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F8.DataAccess;

public sealed class F8Repository : IF8Repository
{
    private readonly AppDbContext _appContext;

    public F8Repository(AppDbContext context)
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
                    var rowsAffected = await _appContext
                        .Set<TodoTaskListEntity>()
                        .Where(list => list.Id == listId)
                        .ExecuteDeleteAsync(ct);

                    if (rowsAffected == 0)
                    {
                        throw new DbUpdateException();
                    }

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
