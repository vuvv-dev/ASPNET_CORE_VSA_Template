using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F13.DataAccess;

public sealed class F13Repository : IF13Repository
{
    private readonly AppDbContext _appContext;

    public F13Repository(AppDbContext context)
    {
        _appContext = context;
    }
}
