using System.Threading;
using System.Threading.Tasks;
using F015.Models;

namespace F015.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<TodoTaskModel> GetTaskDetailByIdAsync(long taskId, CancellationToken ct);
}
