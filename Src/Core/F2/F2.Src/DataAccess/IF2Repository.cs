using System.Threading;
using System.Threading.Tasks;
using F2.Src.Models;

namespace F2.Src.DataAccess;

public interface IF2Repository
{
    Task<F2TodoTaskListModel> GetTodoTaskListAsync(long listId, CancellationToken ct);
}
