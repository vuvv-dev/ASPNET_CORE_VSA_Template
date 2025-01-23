using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F1.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F1.Presentation.Filters.SetStateBag;

public sealed class F1SetStateBagFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var doesRequestExist = context.ActionArguments.Any(argument =>
            argument.Key.Equals(F1Constant.REQUEST_ARGUMENT_NAME)
        );

        if (!doesRequestExist)
        {
            context.Result = new ContentResult
            {
                StatusCode = F1Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F1Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        var stateBag = new F1StateBag
        {
            HttpRequest = context.ActionArguments[F1Constant.REQUEST_ARGUMENT_NAME] as F1Request
        };

        context.HttpContext.Items.Add(nameof(F1StateBag), stateBag);

        await next();
    }
}
