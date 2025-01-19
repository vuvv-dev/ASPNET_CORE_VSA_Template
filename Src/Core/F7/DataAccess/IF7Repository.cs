using System.Threading;
using System.Threading.Tasks;
using F7.Models;

namespace F7.DataAccess;

public interface IF7Repository
{
    Task<bool> CreateTaskTodoListAsync(F7TaskTodoListModel taskTodoList, CancellationToken ct);
}
