using System.Threading;
using System.Threading.Tasks;

namespace F017.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ModifyCompletedStatusAsync(long taskId, bool isCompleted, CancellationToken ct);
}
