using F14.Common;
using Microsoft.AspNetCore.Mvc;

namespace F14.Presentation;

public sealed class F14Request
{
    [FromRoute]
    public long TodoTaskId { get; set; }

    [FromRoute]
    public long TodoTaskListId { get; set; }

    [FromQuery(Name = F14Constant.Url.Query.NumberOfRecord)]
    public int NumberOfRecord { get; set; }
}
