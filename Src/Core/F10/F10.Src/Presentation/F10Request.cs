using F10.Src.Common;
using Microsoft.AspNetCore.Mvc;

namespace F10.Src.Presentation;

public sealed class F10Request
{
    [FromQuery(Name = F10Constant.UrlQuery.ListId)]
    public long TodoTaskListId { get; set; }

    [FromQuery(Name = F10Constant.UrlQuery.NumberOfRecord)]
    public int NumberOfRecord { get; set; }
}
