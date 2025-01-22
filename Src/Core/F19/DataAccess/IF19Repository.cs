using System.Threading;
using System.Threading.Tasks;

namespace F19.DataAccess;

public interface IF19Repository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ChangeContentAsync(long taskId, string content, CancellationToken ct);
}
