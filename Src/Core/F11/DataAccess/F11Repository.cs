using FA1.DbContext;

namespace F11.DataAccess;

public sealed class F11Repository : IF11Repository
{
    private readonly AppDbContext _appContext;

    public F11Repository(AppDbContext context)
    {
        _appContext = context;
    }
}
