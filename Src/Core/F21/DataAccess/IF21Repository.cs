using System;
using System.Threading;
using System.Threading.Tasks;

namespace F21.DataAccess;

public interface IF21Repository
{
    Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct);

    Task<bool> ChangeDueDateAsync(long taskId, DateTime dueDate, CancellationToken ct);
}
