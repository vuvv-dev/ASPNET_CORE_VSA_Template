using System.Threading;
using System.Threading.Tasks;
using F007.Models;

namespace F007.DataAccess;

public interface IRepository
{
    Task<bool> CreateTaskTodoListAsync(TaskTodoListModel taskTodoList, CancellationToken ct);
}
