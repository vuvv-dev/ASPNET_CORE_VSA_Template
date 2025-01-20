using System.Threading;
using System.Threading.Tasks;
using F11.Models;

namespace F11.DataAccess;

public interface IF11Repository
{
    Task<bool> CreateTodoTaskAsync(F11TaskTodoModel todoTask, CancellationToken ct);

    Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct);
}
