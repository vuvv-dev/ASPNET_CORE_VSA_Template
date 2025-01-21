using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F16.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F16.Presentation.Filters.Validation;

public sealed class F16ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F16Request> _validator;

    public F16ValidationFilter(IValidator<F16Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F16Constant.REQUEST_ARGUMENT_NAME] as F16Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F16Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F16Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
