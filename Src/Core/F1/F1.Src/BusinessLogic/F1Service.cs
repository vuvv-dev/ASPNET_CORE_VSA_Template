using System;
using System.Threading;
using System.Threading.Tasks;
using F1.Src.DataAccess;
using F1.Src.Models;
using FCommon.Src.FeatureService;

namespace F1.Src.BusinessLogic;

public sealed class F1Service : IServiceHandler<F1AppRequestModel, F1AppResponseModel>
{
    private readonly Lazy<IF1Repository> _repository;

    public F1Service(Lazy<IF1Repository> repository)
    {
        _repository = repository;
    }

    public Task<F1AppResponseModel> ExecuteAsync(F1AppRequestModel request, CancellationToken ct)
    {
        F1AppResponseModel response = null;

        return Task.FromResult(response);
    }
}
