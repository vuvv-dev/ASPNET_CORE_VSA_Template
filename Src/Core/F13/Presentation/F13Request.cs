using F13.Common;
using Microsoft.AspNetCore.Mvc;

namespace F13.Presentation;

public sealed class F13Request
{
    [FromRoute]
    public long TodoTaskId { get; set; }

    [FromRoute]
    public long TodoTaskListId { get; set; }

    [FromQuery(Name = F13Constant.Url.Query.NumberOfRecord)]
    public int NumberOfRecord { get; set; }
}
