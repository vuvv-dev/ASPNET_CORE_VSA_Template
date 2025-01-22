using System.Threading;
using System.Threading.Tasks;

namespace F20.DataAccess;

public interface IF20Repository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ChangeNoteAsync(long taskId, string note, CancellationToken ct);
}
