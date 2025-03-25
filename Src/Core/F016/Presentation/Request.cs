using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F016.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskId { get; set; }

    public bool IsInMyDay { get; set; }
}
