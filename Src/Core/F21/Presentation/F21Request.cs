using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F21.Presentation;

[ValidateNever]
public sealed class F21Request
{
    public long TodoTaskId { get; set; }

    public DateTime DueDate { get; set; }
}
