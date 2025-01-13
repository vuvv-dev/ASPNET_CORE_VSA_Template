using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F2.Src.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F2.Src.Presentation.Filters.Validation;

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
            var httpResponse = new F2Response
            {
                HttpCode = StatusCodes.Status400BadRequest,
                AppCode = F2Constant.AppCode.VALIDATION_FAILED,
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
