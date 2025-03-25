using System.Threading;
using System.Threading.Tasks;

namespace F018.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ModifyIsImportantStatusAsync(long taskId, bool isImportant, CancellationToken ct);
}
