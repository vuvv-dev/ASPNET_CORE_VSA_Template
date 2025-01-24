using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F8.Presentation;

[ValidateNever]
public sealed class F8Request
{
    [FromRoute]
    public long TodoTaskListId { get; set; }
}
