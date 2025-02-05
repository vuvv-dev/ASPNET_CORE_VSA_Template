using System.Threading;
using System.Threading.Tasks;

namespace F16.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ModifyIsInMyDayStatusAsync(long taskId, bool isInMyDay, CancellationToken ct);
}
