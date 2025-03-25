using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F003.Presentation;

[ValidateNever]
public sealed class Request
{
    public string Email { get; set; }

    public string Password { get; set; }
}
