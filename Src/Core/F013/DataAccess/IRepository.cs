using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using F013.Models;

namespace F013.DataAccess;

public interface IRepository
{
    Task<IEnumerable<TodoTaskModel>> GetUncompletedTodoTasksAsync(
        GetTodoTasksInputModel input,
        CancellationToken ct
    );

    Task<bool> DoesTodoTaskExistAsync(long taskId, long listId, CancellationToken ct);

    Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct);
}
