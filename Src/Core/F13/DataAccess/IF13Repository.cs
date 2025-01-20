using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using F13.Models;

namespace F13.DataAccess;

public interface IF13Repository
{
    Task<IEnumerable<F13TodoTaskModel>> GetTodoTasksAsync(
        F13GetTodoTasksInputModel input,
        CancellationToken ct
    );

    Task<bool> DoesTodoTaskExistAsync(long taskId, long listId, CancellationToken ct);

    Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct);
}
