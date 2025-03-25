using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F021.Presentation;

[ValidateNever]
public sealed class Request
{
    public long TodoTaskId { get; set; }

    public DateTime DueDate { get; set; }
}
