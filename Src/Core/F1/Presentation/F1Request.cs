using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F1.Presentation;

[ValidateNever]
public sealed class F1Request
{
    public string Email { get; init; }

    public string Password { get; init; }

    public bool RememberMe { get; init; }
}
