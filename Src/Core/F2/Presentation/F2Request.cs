using Microsoft.AspNetCore.Mvc;

namespace F2.Presentation;

public sealed class F2Request
{
    [FromRoute]
    public long TodoTaskListId { get; init; }
}
