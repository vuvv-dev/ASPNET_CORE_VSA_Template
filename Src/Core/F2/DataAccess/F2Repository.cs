using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F2.Models;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F2.DataAccess;

public sealed class F2Repository : IF2Repository
{
    private readonly AppDbContext _appContext;

    public F2Repository(AppDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<F2TodoTaskListModel> GetTodoTaskListAsync(long listId, CancellationToken ct)
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

        var todoTaskListModel = new F2TodoTaskListModel { Name = foundTodoTaskList.Name };

        return todoTaskListModel;
    }
}
