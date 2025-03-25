using System.Threading;
using System.Threading.Tasks;

namespace F012.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> RemoveTodoTaskAsync(long taskId, CancellationToken ct);
}
