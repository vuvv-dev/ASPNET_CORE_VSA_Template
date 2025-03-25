using System.Threading;
using System.Threading.Tasks;

namespace F016.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ModifyIsInMyDayStatusAsync(long taskId, bool isInMyDay, CancellationToken ct);
}
