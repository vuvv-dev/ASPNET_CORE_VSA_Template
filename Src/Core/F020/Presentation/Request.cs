using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F020.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskId { get; set; }

    public string Note { get; set; }
}
