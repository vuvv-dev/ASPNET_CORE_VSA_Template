using Microsoft.AspNetCore.Mvc;

namespace F8.Src.Presentation;

public sealed class F8Request
{
    [FromRoute]
    public long TodoTaskListId { get; set; }
}
