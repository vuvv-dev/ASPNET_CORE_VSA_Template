using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.FX001.DbContext;
using Base.FX001.Entities;
using F002.Models;
using Microsoft.EntityFrameworkCore;

namespace F002.DataAccess;

public sealed class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<TodoTaskListModel> GetTodoTaskListAsync(long listId, CancellationToken ct)
    {
        var foundTodoTaskList = await _appContext
            .Set<TodoTaskListEntity>()
            .AsNoTracking()
            .Where(entity => entity.Id == listId)
            .Select(entity => new TodoTaskListEntity { Name = entity.Name })
            .FirstOrDefaultAsync(ct);

        if (Equals(foundTodoTaskList, null))
        {
            return null;
        }

        var todoTaskListModel = new TodoTaskListModel { Name = foundTodoTaskList.Name };

        return todoTaskListModel;
    }
}
