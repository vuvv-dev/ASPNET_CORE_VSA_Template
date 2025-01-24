using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F20.Presentation;

[ValidateNever]
public sealed class F20Request
{
    public long TodoTaskId { get; set; }

    public string Note { get; set; }
}
