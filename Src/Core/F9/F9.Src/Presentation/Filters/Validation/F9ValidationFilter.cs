using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F9.Src.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F9.Src.Presentation.Filters.Validation;

public sealed class F9ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F9Request> _validator;

    public F9ValidationFilter(IValidator<F9Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F9Constant.REQUEST_ARGUMENT_NAME] as F9Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F9Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F9Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
