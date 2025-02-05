using System;
using System.Threading;
using System.Threading.Tasks;
using F15.Common;
using F15.DataAccess;
using F15.Models;
using FCommon.FeatureService;

namespace F15.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;

    public Service(Lazy<IRepository> repository)
    {
        _repository = repository;
    }

    public async Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        var doesTaskExist = await _repository.Value.DoesTodoTaskExistAsync(request.TodoTaskId, ct);
        if (!doesTaskExist)
        {
            return Constant.DefaultResponse.App.TASK_NOT_FOUND;
        }

        var taskDetail = await _repository.Value.GetTaskDetailByIdAsync(request.TodoTaskId, ct);
        var appResponse = new AppResponseModel()
        {
            AppCode = Constant.AppCode.SUCCESS,
            Body = new()
            {
                TodoTask = new()
                {
                    Id = request.TodoTaskId,
                    Content = taskDetail.Content,
                    DueDate = taskDetail.DueDate,
                    IsExpired = taskDetail.IsExpired,
                    IsCompleted = taskDetail.IsCompleted,
                    IsImportant = taskDetail.IsImportant,
                    IsInMyDay = taskDetail.IsInMyDay,
                    Note = taskDetail.Note,
                },
            },
        };

        return appResponse;
    }
}
