using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F16.Presentation;

[ValidateNever]
public sealed class F16Request
{
    public long TodoTaskId { get; set; }

    public bool IsInMyDay { get; set; }
}
