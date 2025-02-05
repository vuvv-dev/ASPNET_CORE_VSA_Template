using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F7.Presentation;

[ValidateNever]
public sealed class Request
{
    public string TodoTaskListName { get; set; }
}
