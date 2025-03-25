using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F002.Presentation;

[ValidateNever]
public sealed class Request
{
    [FromRoute]
    public long TodoTaskListId { get; init; }
}
