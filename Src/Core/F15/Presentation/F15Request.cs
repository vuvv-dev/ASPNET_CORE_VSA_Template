using Microsoft.AspNetCore.Mvc;

namespace F15.Presentation;

public sealed class F15Request
{
    [FromRoute]
    public long TodoTaskId { get; set; }
}
