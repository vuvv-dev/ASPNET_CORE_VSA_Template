using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace F2.Src.Presentation.Filters;

public sealed class F2ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceScopeFactory _scopeFactory;

    public F2ValidationFilter(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        await next();
    }
}
