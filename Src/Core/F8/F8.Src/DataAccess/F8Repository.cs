using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace F8.Src.DataAccess;

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
                        .Where(token => token.Id.Equals(listId))
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
