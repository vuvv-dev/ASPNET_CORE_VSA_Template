using System.Threading;
using System.Threading.Tasks;
using F009.Models;

namespace F009.DataAccess;

public interface IRepository
{
    Task<bool> DoesTaskTodoListExistAsync(long listId, CancellationToken ct);

    Task<bool> UpdateTaskTodoListAsync(TaskTodoListModel taskTodoList, CancellationToken ct);
}
