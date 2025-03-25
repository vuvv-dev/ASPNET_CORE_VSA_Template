using System.Threading;
using System.Threading.Tasks;
using F011.Models;

namespace F011.DataAccess;

public interface IRepository
{
    Task<bool> CreateTodoTaskAsync(TaskTodoModel todoTask, CancellationToken ct);

    Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct);
}
