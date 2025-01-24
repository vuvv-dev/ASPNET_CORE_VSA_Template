using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F3.Presentation;

[ValidateNever]
public sealed class F3Request
{
    public string Email { get; set; }

    public string Password { get; set; }
}
