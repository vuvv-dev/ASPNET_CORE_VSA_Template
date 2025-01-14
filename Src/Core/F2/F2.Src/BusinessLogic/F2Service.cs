using System;
using System.Threading;
using System.Threading.Tasks;
using F2.Src.Common;
using F2.Src.DataAccess;
using F2.Src.Models;
using FCommon.Src.FeatureService;

namespace F2.Src.BusinessLogic;

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
        var list = await _repository.Value.GetTodoTaskListAsync(request.ListId, ct);
        if (Equals(list, null))
        {
            return F2Constant.DefaultResponse.App.LIST_NOT_FOUND;
        }

        return new()
        {
            AppCode = F2Constant.AppCode.SUCCESS,
            Body = new()
            {
                Id = request.ListId,
                Name = list.Name,
                CreatedDate = list.CreatedDate,
            },
        };
    }
}
