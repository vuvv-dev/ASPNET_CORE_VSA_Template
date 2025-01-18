using System;
using System.Threading;
using System.Threading.Tasks;
using F7.Src.Common;
using F7.Src.DataAccess;
using F7.Src.Models;
using FCommon.Src.FeatureService;
using FCommon.Src.IdGeneration;

namespace F7.Src.BusinessLogic;

public sealed class F7Service : IServiceHandler<F7AppRequestModel, F7AppResponseModel>
{
    private readonly Lazy<IF7Repository> _repository;
    private readonly Lazy<IAppIdGenerator> _idGenerator;

    public F7Service(Lazy<IF7Repository> repository, Lazy<IAppIdGenerator> idGenerator)
    {
        _repository = repository;
        _idGenerator = idGenerator;
    }

    public async Task<F7AppResponseModel> ExecuteAsync(
        F7AppRequestModel request,
        CancellationToken ct
    )
    {
        var newList = new F7TaskTodoListModel
        {
            Id = _idGenerator.Value.NextId(),
            Name = request.TodoTaskListName,
            CreatedDate = DateTime.UtcNow,
            UserId = request.UserId,
        };

        var isCreated = await _repository.Value.CreateTaskTodoListAsync(newList, ct);
        if (!isCreated)
        {
            return F7Constant.DefaultResponse.App.SERVER_ERROR;
        }

        return new()
        {
            AppCode = F7Constant.AppCode.SUCCESS,
            Body = new() { ListId = newList.Id },
        };
    }
}
