using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F15.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F15.Presentation.Filters.Validation;

public sealed class F15ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F15Request> _validator;

    public F15ValidationFilter(IValidator<F15Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F15Constant.REQUEST_ARGUMENT_NAME] as F15Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F15Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F15Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
