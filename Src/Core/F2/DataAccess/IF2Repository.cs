using System.Threading;
using System.Threading.Tasks;
using F2.Models;

namespace F2.DataAccess;

public interface IF2Repository
{
    Task<F2TodoTaskListModel> GetTodoTaskListAsync(long listId, CancellationToken ct);
}
