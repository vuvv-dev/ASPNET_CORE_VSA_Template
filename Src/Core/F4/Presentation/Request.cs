using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F4.Presentation;

[ValidateNever]
public sealed class Request
{
    public string Email { get; set; }
}
