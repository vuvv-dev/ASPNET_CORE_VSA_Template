using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F006.Presentation;

[ValidateNever]
public sealed class Request
{
    public string RefreshToken { get; set; }
}
