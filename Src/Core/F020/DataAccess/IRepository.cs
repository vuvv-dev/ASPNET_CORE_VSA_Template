using System.Threading;
using System.Threading.Tasks;

namespace F020.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ChangeNoteAsync(long taskId, string note, CancellationToken ct);
}
