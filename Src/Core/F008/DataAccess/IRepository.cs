using System.Threading;
using System.Threading.Tasks;

namespace F008.DataAccess;

public interface IRepository
{
    Task<bool> DoesTaskTodoListExistAsync(long listId, CancellationToken ct);

    Task<bool> RemoveTaskTodoListAsync(long listId, CancellationToken ct);
}
