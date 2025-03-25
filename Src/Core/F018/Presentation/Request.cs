using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F018.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskId { get; set; }

    public bool IsImportant { get; set; }
}
