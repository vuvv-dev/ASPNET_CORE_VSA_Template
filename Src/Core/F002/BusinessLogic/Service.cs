using System;
using System.Threading;
using System.Threading.Tasks;
using F002.Common;
using F002.DataAccess;
using F002.Models;
using FCommon.FeatureService;

namespace F002.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;

    public Service(Lazy<IRepository> repository)
    {
        _repository = repository;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var list = await _repository.Value.GetTodoTaskListAsync(request.TodoTaskListId, ct);
        if (Equals(list, null))
        {
            return Constant.DefaultResponse.App.LIST_NOT_FOUND;
        }

        return new()
        {
            AppCode = Constant.AppCode.SUCCESS,
            Body = new()
            {
                TodoTaskList = new() { Id = request.TodoTaskListId, Name = list.Name },
            },
        };
    }
}
