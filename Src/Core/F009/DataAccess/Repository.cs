using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using F009.Models;
using Microsoft.EntityFrameworkCore;

namespace F009.DataAccess;

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

    public async Task<bool> UpdateTaskTodoListAsync(
        TaskTodoListModel taskTodoList,
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
                        .Set<TodoTaskListEntity>()
                        .Where(token => token.Id.Equals(taskTodoList.Id))
                        .ExecuteUpdateAsync(setProp =>
                            setProp.SetProperty(entity => entity.Name, taskTodoList.Name)
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
}
