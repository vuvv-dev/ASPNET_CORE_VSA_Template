using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F3.Src.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F3.Src.Presentation.Filters.Validation;

public sealed class F3ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F3Request> _validator;

    public F3ValidationFilter(IValidator<F3Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F3Constant.REQUEST_ARGUMENT_NAME] as F3Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F3Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F3Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
