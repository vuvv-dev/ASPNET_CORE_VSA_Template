using System.Net.Mime;
using System.Threading.Tasks;
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
        const string RequestKey = "request";

        var request = context.ActionArguments[RequestKey] as F1Request;

        var result = await _validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Content = "Invalid request, validation failed",
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
