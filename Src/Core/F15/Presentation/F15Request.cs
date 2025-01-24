using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F15.Presentation;

[ValidateNever]
public sealed class F15Request
{
    [FromRoute]
    public long TodoTaskId { get; set; }
}
