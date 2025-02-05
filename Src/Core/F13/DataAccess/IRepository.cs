using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using F13.Models;

namespace F13.DataAccess;

public interface IRepository
{
    Task<IEnumerable<TodoTaskModel>> GetTodoTasksAsync(
        GetTodoTasksInputModel input,
        CancellationToken ct
    );

    Task<bool> DoesTodoTaskExistAsync(long taskId, long listId, CancellationToken ct);

    Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct);
}
