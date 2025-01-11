using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FA1.Src.DbContext;
using FA1.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace F2.Src.DataAccess;

public sealed class F2Repository : IF2Repository
{
    private readonly Lazy<AppDbContext> _appContext;

    public F2Repository(Lazy<AppDbContext> appContext)
    {
        _appContext = appContext;
    }

    public async Task<TodoTaskListEntity> GetTodoTaskListAsync(long listId, CancellationToken ct)
    {
        return await _appContext
            .Value.Set<TodoTaskListEntity>()
            .AsNoTracking()
            .Where(entity => entity.Id == listId)
            .Select(entity => new TodoTaskListEntity
            {
                Name = entity.Name,
                CreatedDate = entity.CreatedDate,
                UserId = entity.UserId,
            })
            .FirstOrDefaultAsync(ct);
    }
}
