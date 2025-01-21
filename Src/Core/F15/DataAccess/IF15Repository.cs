using System.Threading;
using System.Threading.Tasks;
using F15.Models;

namespace F15.DataAccess;

public interface IF15Repository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<F15TodoTaskModel> GetTaskDetailByIdAsync(long taskId, CancellationToken ct);
}
