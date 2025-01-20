using Microsoft.AspNetCore.Mvc;

namespace F12.Presentation;

public sealed class F12Request
{
    [FromRoute]
    public long TodoTaskId { get; set; }
}
