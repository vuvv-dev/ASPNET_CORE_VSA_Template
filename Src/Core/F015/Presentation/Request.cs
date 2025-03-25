using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F015.Presentation;

[ValidateNever]
public sealed class Request
{
    [FromRoute]
    public long TodoTaskId { get; set; }
}
