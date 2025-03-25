using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F001.Presentation;

[ValidateNever]
public sealed class Request
{
    public string Email { get; init; }

    public string Password { get; init; }

    public bool RememberMe { get; init; }
}
