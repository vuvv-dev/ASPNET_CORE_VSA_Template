using System;
using System.Threading;
using System.Threading.Tasks;
using F7.Common;
using F7.DataAccess;
using F7.Models;
using FCommon.FeatureService;
using FCommon.IdGeneration;

namespace F7.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public Service(Lazy<IRepository> repository, Lazy<IAppIdGenerator> idGenerator)
    {
        _repository = repository;
        _idGenerator = idGenerator;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var newList = new TaskTodoListModel
        {
            Id = _idGenerator.Value.NextId(),
            Name = request.TodoTaskListName,
            CreatedDate = DateTime.UtcNow,
            UserId = request.UserId,
        };

        var isCreated = await _repository.Value.CreateTaskTodoListAsync(newList, ct);
        if (!isCreated)
        {
            return Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new()
        {
            AppCode = Constant.AppCode.SUCCESS,
            Body = new() { ListId = newList.Id },
        };
    }
}
