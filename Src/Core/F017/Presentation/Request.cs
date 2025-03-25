using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F017.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskId { get; set; }

    public bool IsCompleted { get; set; }
}
