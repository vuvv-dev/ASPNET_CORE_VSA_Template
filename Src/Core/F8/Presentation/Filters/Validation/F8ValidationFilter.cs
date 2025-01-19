using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F8.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F8.Presentation.Filters.Validation;

public sealed class F8ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F8Request> _validator;

    public F8ValidationFilter(IValidator<F8Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F8Constant.REQUEST_ARGUMENT_NAME] as F8Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F8Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F8Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
