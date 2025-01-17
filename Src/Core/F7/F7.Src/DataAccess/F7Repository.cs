using FA1.Src.DbContext;

namespace F7.Src.DataAccess;

public sealed class F7Repository : IF7Repository
{
    private readonly AppDbContext _appContext;

    public F7Repository(AppDbContext context)
    {
        _appContext = context;
    }
}
