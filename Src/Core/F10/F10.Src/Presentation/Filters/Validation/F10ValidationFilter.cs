using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F10.Src.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F10.Src.Presentation.Filters.Validation;

public sealed class F10ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F10Request> _validator;

    public F10ValidationFilter(IValidator<F10Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F10Constant.REQUEST_ARGUMENT_NAME] as F10Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F10Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F10Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
