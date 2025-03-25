using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using F007.Models;
using Microsoft.EntityFrameworkCore;

namespace F007.DataAccess;

public sealed partial class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<bool> CreateTaskTodoListAsync(
        TaskTodoListModel taskTodoList,
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
}
