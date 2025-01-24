using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F5.Presentation;

[ValidateNever]
public sealed class F5Request
{
    public string NewPassword { get; set; }
}
