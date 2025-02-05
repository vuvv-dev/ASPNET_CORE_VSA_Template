using System.Threading;
using System.Threading.Tasks;
using F9.Models;

namespace F9.DataAccess;

public interface IRepository
{
    Task<bool> DoesTaskTodoListExistAsync(long listId, CancellationToken ct);

    Task<bool> UpdateTaskTodoListAsync(TaskTodoListModel taskTodoList, CancellationToken ct);
}
