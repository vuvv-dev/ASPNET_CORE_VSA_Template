using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F005.Presentation;

[ValidateNever]
public sealed class Request
{
    public string NewPassword { get; set; }
}
