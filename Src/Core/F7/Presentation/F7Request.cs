using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F7.Presentation;

[ValidateNever]
public sealed class F7Request
{
    public string TodoTaskListName { get; set; }
}
