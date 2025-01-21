using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using F14.Models;

namespace F14.DataAccess;

public interface IF14Repository
{
    Task<IEnumerable<F14TodoTaskModel>> GetTodoTasksAsync(
        F14GetTodoTasksInputModel input,
        CancellationToken ct
    );

    Task<bool> DoesTodoTaskExistAsync(long taskId, long listId, CancellationToken ct);

    Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct);
}
