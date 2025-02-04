using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F6.Presentation;

[ValidateNever]
public sealed class Request
{
    public string RefreshToken { get; set; }
}
