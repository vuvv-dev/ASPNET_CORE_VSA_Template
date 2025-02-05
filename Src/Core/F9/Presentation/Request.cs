using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F9.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskListId { get; set; }

    public string NewName { get; set; }
}
