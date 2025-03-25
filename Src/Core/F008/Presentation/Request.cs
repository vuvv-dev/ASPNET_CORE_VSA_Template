using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F008.Presentation;

[ValidateNever]
public sealed class Request
{
    [FromRoute]
    public long TodoTaskListId { get; set; }
}
