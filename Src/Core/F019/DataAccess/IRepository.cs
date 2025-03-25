using System.Threading;
using System.Threading.Tasks;

namespace F019.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ChangeContentAsync(long taskId, string content, CancellationToken ct);
}
