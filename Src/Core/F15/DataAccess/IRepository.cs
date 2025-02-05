using System.Threading;
using System.Threading.Tasks;
using F15.Models;

namespace F15.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<TodoTaskModel> GetTaskDetailByIdAsync(long taskId, CancellationToken ct);
}
