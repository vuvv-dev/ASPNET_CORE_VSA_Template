using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F006.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F006.Presentation.Filters.SetStateBag;

public sealed class SetStateBagFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var doesRequestExist = context.ActionArguments.Any(argument =>
            argument.Key.Equals(Constant.REQUEST_ARGUMENT_NAME)
        );

        if (!doesRequestExist)
        {
            context.Result = new ContentResult
            {
                StatusCode = Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(Constant.DefaultResponse.Http.VALIDATION_FAILED),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        var stateBag = context.HttpContext.Items[nameof(StateBag)] as StateBag;
        stateBag.HttpRequest = context.ActionArguments[Constant.REQUEST_ARGUMENT_NAME] as Request;

        await next();
    }
}
