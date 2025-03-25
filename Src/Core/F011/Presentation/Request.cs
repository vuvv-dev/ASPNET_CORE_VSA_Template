using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F011.Presentation;

[ValidateNever]
public sealed class Request
{
    public string Content { get; set; }

    public long TodoTaskListId { get; set; }
}
