using System.Threading;
using System.Threading.Tasks;
using FA1.Src.Entities;

namespace F2.Src.DataAccess;

public interface IF2Repository
{
    Task<TodoTaskListEntity> GetTodoTaskListAsync(long listId, CancellationToken ct);
}
