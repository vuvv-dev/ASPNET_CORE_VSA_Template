using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F009.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskListId { get; set; }

    public string NewName { get; set; }
}
