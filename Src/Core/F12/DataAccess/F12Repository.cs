using FA1.DbContext;

namespace F12.DataAccess;

public sealed class F12Repository : IF12Repository
{
    private readonly AppDbContext _appContext;

    public F12Repository(AppDbContext context)
    {
        _appContext = context;
    }
}
