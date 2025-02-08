using System;
using System.Threading;
using System.Threading.Tasks;
using FeatTemplate.DataAccess;
using FeatTemplate.Models;
using RFeatureCommonModule.FeatureService;

namespace FeatTemplate.BusinessLogic;

public sealed class Service : IServiceHandler<AppRequestModel, AppResponseModel>
{
    private readonly Lazy<IRepository> _repository;

    public Service(Lazy<IRepository> repository)
    {
        _repository = repository;
    }

    public Task<AppResponseModel> ExecuteAsync(AppRequestModel request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
