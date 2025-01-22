using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F18.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F18.Presentation.Filters.Validation;

public sealed class F18ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F18Request> _validator;

    public F18ValidationFilter(IValidator<F18Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F18Constant.REQUEST_ARGUMENT_NAME] as F18Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F18Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F18Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
