using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F18.Presentation;

[ValidateNever]
public sealed class F18Request
{
    public long TodoTaskId { get; set; }

    public bool IsImportant { get; set; }
}
