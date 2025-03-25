using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using F010.Models;

namespace F010.DataAccess;

public interface IRepository
{
    Task<IEnumerable<TodoTaskListModel>> GetTodoTaskListAsync(
        long todoTaskListId,
        int numberOfRecord,
        CancellationToken ct
    );

    Task<bool> DoesTodoTaskListExistAsync(long listId, CancellationToken ct);
}
