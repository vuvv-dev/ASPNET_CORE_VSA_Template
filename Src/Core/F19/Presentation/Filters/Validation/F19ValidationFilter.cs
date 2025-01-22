using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F19.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F19.Presentation.Filters.Validation;

public sealed class F19ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F19Request> _validator;

    public F19ValidationFilter(IValidator<F19Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F19Constant.REQUEST_ARGUMENT_NAME] as F19Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F19Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F19Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
