using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F019.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskId { get; set; }

    public string Content { get; set; }
}
