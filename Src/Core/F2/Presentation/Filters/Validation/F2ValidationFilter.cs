using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F2.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F2.Presentation.Filters.Validation;

public sealed class F2ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F2Request> _validator;

    public F2ValidationFilter(IValidator<F2Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F2Constant.REQUEST_ARGUMENT_NAME] as F2Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F2Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F2Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
