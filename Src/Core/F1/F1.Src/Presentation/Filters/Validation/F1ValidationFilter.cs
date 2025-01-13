using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F1.Src.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F1.Src.Presentation.Filters.Validation;

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
            var httpResponse = new F1Response
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = F1Constant.AppCode.VALIDATION_FAILED,
            };

            context.Result = new ContentResult
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Content = JsonSerializer.Serialize(httpResponse),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
