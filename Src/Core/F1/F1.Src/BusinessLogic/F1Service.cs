using System;
using F1.Src.DataAccess;

namespace F1.Src.BusinessLogic;

public sealed class F1Service
{
    private readonly Lazy<IF1Repository> _repository;

    public F1Service(Lazy<IF1Repository> repository)
    {
        _repository = repository;
    }
}
