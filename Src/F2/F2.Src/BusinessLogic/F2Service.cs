using System;
using F2.Src.DataAccess;

namespace F2.Src.BusinessLogic;

public sealed class F2Service
{
    private readonly Lazy<IF2Repository> _repository;

    public F2Service(Lazy<IF2Repository> repository)
    {
        _repository = repository;
    }

    public (int appCode, string value) Execute()
    {
        return (F2Common.AppCode.SUCCESS, _repository.Value.GetUser());
    }
}
