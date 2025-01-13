using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F2.Src.Models;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace F2.Src.DataAccess;

public sealed class F2Repository : IF2Repository
{
    private readonly AppDbContext _appContext;

    public F2Repository(AppDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<F2TodoTaskListModel> GetTodoTaskListAsync(long listId, CancellationToken ct)
    {
        return await _appContext
            .Set<TodoTaskListEntity>()
            .AsNoTracking()
            .Where(entity => entity.Id == listId)
            .Select(entity => new F2TodoTaskListModel { Name = entity.Name })
            .FirstOrDefaultAsync(ct);
    }
}
