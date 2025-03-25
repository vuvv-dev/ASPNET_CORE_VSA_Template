using System;
using System.Threading;
using System.Threading.Tasks;

namespace F021.DataAccess;

public interface IRepository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ChangeDueDateAsync(long taskId, DateTime dueDate, CancellationToken ct);
}
