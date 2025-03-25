using System.Threading;
using System.Threading.Tasks;
using F002.Models;

namespace F002.DataAccess;

public interface IRepository
{
    Task<TodoTaskListModel> GetTodoTaskListAsync(long listId, CancellationToken ct);
}
