using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using F10.Src.Models;

namespace F10.Src.DataAccess;

public interface IF10Repository
{
    Task<IEnumerable<F10TodoTaskListModel>> GetTodoTaskListAsync(
        long todoTaskListId,
        int numberOfRecord,
        CancellationToken ct
    );
}
