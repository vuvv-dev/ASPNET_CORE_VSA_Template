using FA1.Src.DbContext;

namespace F8.Src.DataAccess;

public sealed class F8Repository : IF8Repository
{
    private readonly AppDbContext _appContext;

    public F8Repository(AppDbContext context)
    {
        _appContext = context;
    }
}
