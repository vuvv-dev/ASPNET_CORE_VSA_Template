using System.Threading;
using System.Threading.Tasks;

namespace F8.DataAccess;

public interface IF8Repository
{
    Task<bool> DoesTaskTodoListExistAsync(long listId, CancellationToken ct);

    Task<bool> RemoveTaskTodoListAsync(long listId, CancellationToken ct);
}
