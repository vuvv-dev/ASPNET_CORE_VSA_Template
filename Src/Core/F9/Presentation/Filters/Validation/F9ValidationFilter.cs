using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using F9.Common;
using F9.Presentation.Filters.SetStateBag;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace F9.Presentation.Filters.Validation;

public sealed class F9ValidationFilter : IAsyncActionFilter
{
    private readonly IValidator<F9Request> _validator;

    public F9ValidationFilter(IValidator<F9Request> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var stateBag = context.HttpContext.Items[nameof(F9StateBag)] as F9StateBag;
        var request = stateBag.HttpRequest;

        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            context.Result = new ContentResult
            {
                StatusCode = F9Constant.DefaultResponse.Http.VALIDATION_FAILED.HttpCode,
                Content = JsonSerializer.Serialize(
                    F9Constant.DefaultResponse.Http.VALIDATION_FAILED
                ),
                ContentType = MediaTypeNames.Application.Json,
            };

            return;
        }

        await next();
    }
}
