using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F4.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F4.Presentation.Filters.Validation;

public sealed class F4ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F4Request> _validator;

    public F4ValidationFilter(IValidator<F4Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F4Constant.REQUEST_ARGUMENT_NAME] as F4Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F4Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F4Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
