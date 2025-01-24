using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F12.Presentation;

[ValidateNever]
public sealed class F12Request
{
    [FromRoute]
    public long TodoTaskId { get; set; }
}
