using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F007.Presentation;

[ValidateNever]
public sealed class Request
{
    public string TodoTaskListName { get; set; }
}
