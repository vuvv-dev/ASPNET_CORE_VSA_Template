using System.Threading;
using System.Threading.Tasks;

namespace F17.DataAccess;

public interface IF17Repository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ModifyCompletedStatusAsync(long taskId, bool isCompleted, CancellationToken ct);
}
