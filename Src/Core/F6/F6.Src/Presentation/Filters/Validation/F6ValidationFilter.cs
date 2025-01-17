using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F6.Src.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F6.Src.Presentation.Filters.Validation;

public sealed class F6ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F6Request> _validator;

    public F6ValidationFilter(IValidator<F6Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var request = context.ActionArguments[F6Constant.REQUEST_ARGUMENT_NAME] as F6Request;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F6Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F6Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
