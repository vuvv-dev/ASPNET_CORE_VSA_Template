using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F2.Common;
using F2.DataAccess;
using F2.Models;
using FCommon.FeatureService;

namespace F2.BusinessLogic;

public sealed class F2Service : IServiceHandler<F2AppRequestModel, F2AppResponseModel>
{
    private readonly Lazy<IF2Repository> _repository;

    public F2Service(Lazy<IF2Repository> repository)
    {
        _repository = repository;
    }

    public async Task<F2AppResponseModel> ExecuteAsync(
        F2AppRequestModel request,
        CancellationToken ct
    )
    {
        var list = await _repository.Value.GetTodoTaskListAsync(request.TodoTaskListId, ct);
        if (Equals(list, null))
        {
            return F2Constant.DefaultResponse.App.LIST_NOT_FOUND;
        }

        return new()
        {
            AppCode = F2Constant.AppCode.SUCCESS,
            Body = new()
            {
                Id = request.TodoTaskListId,
                Name = list.Name,
                TodoTasks = list.TodoTasks.Select(
                    model => new F2AppResponseModel.BodyModel.TodoTaskModel
                    {
                        Id = model.Id,
                        Name = model.Name,
                        DueDate = model.DueDate,
                        IsInMyDay = model.IsInMyDay,
                        IsImportant = model.IsImportant,
                        IsFinished = model.IsFinished,
                    }
                ),
            },
        };
    }
}
