using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F7.Src.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F7.Src.Presentation.Filters.Validation;

public sealed class F7ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F7Request> _validator;

    public F7ValidationFilter(IValidator<F7Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F7Constant.REQUEST_ARGUMENT_NAME] as F7Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F7Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F7Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
