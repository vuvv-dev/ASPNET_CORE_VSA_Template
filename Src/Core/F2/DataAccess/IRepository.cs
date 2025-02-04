using System.Threading;
using System.Threading.Tasks;
using F2.Models;

namespace F2.DataAccess;

public interface IRepository
{
    Task<TodoTaskListModel> GetTodoTaskListAsync(long listId, CancellationToken ct);
}
