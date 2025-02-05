using System.Threading;
using System.Threading.Tasks;
using F11.Models;

namespace F11.DataAccess;

public interface IRepository
{
    Task<bool> CreateTodoTaskAsync(TaskTodoModel todoTask, CancellationToken ct);

    Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct);
}
