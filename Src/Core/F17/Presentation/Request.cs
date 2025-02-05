using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F17.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskId { get; set; }

    public bool IsCompleted { get; set; }
}
