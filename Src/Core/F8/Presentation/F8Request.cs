using Microsoft.AspNetCore.Mvc;

namespace F8.Presentation;

public sealed class F8Request
{
    [FromRoute]
    public long TodoTaskListId { get; set; }
}
