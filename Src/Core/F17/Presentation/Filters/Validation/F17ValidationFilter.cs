using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F17.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F17.Presentation.Filters.Validation;

public sealed class F17ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F17Request> _validator;

    public F17ValidationFilter(IValidator<F17Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F17Constant.REQUEST_ARGUMENT_NAME] as F17Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F17Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F17Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
