using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F004.Presentation;

[ValidateNever]
public sealed class Request
{
    public string Email { get; set; }
}
