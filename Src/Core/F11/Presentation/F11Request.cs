using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F11.Presentation;

[ValidateNever]
public sealed class F11Request
{
    public string Content { get; set; }

    public long TodoTaskListId { get; set; }
}
