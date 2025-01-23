using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F20.Common;
using F20.Presentation.Filters.SetStateBag;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F20.Presentation.Filters.Validation;

public sealed class F20ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F20Request> _validator;

    public F20ValidationFilter(IValidator<F20Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var stateBag = context.HttpContext.Items[nameof(F20StateBag)] as F20StateBag;
        var request = stateBag.HttpRequest;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F20Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F20Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
