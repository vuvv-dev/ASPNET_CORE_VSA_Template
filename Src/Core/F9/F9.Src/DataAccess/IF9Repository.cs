using System.Threading;
using System.Threading.Tasks;
using F9.Src.Models;

namespace F9.Src.DataAccess;

public interface IF9Repository
{
    Task<bool> DoesTaskTodoListExistAsync(long listId, CancellationToken ct);

    Task<bool> UpdateTaskTodoListAsync(F9TaskTodoListModel taskTodoList, CancellationToken ct);
}
