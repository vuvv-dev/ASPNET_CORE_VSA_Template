using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F12.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F12.Presentation.Filters.Validation;

public sealed class F12ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F12Request> _validator;

    public F12ValidationFilter(IValidator<F12Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F12Constant.REQUEST_ARGUMENT_NAME] as F12Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F12Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F12Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
