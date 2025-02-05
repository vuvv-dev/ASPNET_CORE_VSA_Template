using System.Threading;
using System.Threading.Tasks;
using F7.Models;

namespace F7.DataAccess;

public interface IRepository
{
    Task<bool> CreateTaskTodoListAsync(TaskTodoListModel taskTodoList, CancellationToken ct);
}
