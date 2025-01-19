using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F1.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F1.Presentation.Filters.Validation;

public sealed class F1ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F1Request> _validator;

    public F1ValidationFilter(IValidator<F1Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F1Constant.REQUEST_ARGUMENT_NAME] as F1Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
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

        await next();
    }
}
