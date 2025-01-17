using System.Threading;
using System.Threading.Tasks;
using F7.Src.Models;
using FA1.Src.Common;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace F7.Src.DataAccess;

public sealed class F7Repository : IF7Repository
{
    private readonly AppDbContext _appContext;

    public F7Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<bool> CreateTaskTodoListAsync(
        F7TaskTodoListModel taskTodoList,
        CancellationToken ct
    )
    {
        var taskTodoListEntity = new TodoTaskListEntity
        {
            Id = taskTodoList.Id,
            Name = taskTodoList.Name,
            CreatedDate = taskTodoList.CreatedDate,
            UserId = taskTodoList.UserId,
        };

        try
        {
            await _appContext.Set<TodoTaskListEntity>().AddAsync(taskTodoListEntity, ct);

            await _appContext.SaveChangesAsync(ct);

            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public Task<bool> DoesTaskTodoListExistAsync(string taskTodoListName, CancellationToken ct)
    {
        var upperTaskTodoListName = taskTodoListName.ToUpper();

        return _appContext
            .Set<TodoTaskListEntity>()
            .AnyAsync(entity => entity.Name.ToUpper().Equals(upperTaskTodoListName), ct);
    }
}
