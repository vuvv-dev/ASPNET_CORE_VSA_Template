using System.Threading;
using System.Threading.Tasks;
using F7.Src.Models;

namespace F7.Src.DataAccess;

public interface IF7Repository
{
    Task<bool> CreateTaskTodoListAsync(F7TaskTodoListModel taskTodoList, CancellationToken ct);
}
