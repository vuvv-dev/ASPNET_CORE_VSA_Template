using System.Threading;
using System.Threading.Tasks;

namespace F18.DataAccess;

public interface IF18Repository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ModifyIsImportantStatusAsync(long taskId, bool isImportant, CancellationToken ct);
}
