using System;
using System.Threading;
using System.Threading.Tasks;
using F2.Src.Common;
using F2.Src.DataAccess;
using FA1.Src.Entities;

namespace F2.Src.BusinessLogic;

public sealed class F2Service
{
    private readonly Lazy<IF2Repository> _repository;

    public F2Service(Lazy<IF2Repository> repository)
    {
        _repository = repository;
    }

    public async Task<(int appCode, TodoTaskListEntity todoTaskList)> ExecuteAsync(
        long listId,
        CancellationToken ct
    )
    {
        var list = await _repository.Value.GetTodoTaskListAsync(listId, ct);

        if (Equals(list, null))
        {
            return (F2Constant.AppCode.LIST_NOT_FOUND, null);
        }

        return (F2Constant.AppCode.SUCCESS, list);
    }
}
