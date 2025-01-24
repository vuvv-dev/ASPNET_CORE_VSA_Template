using F13.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F13.Presentation;

[ValidateNever]
public sealed class F13Request
{
    [FromRoute]
    public long TodoTaskId { get; set; }

    [FromRoute]
    public long TodoTaskListId { get; set; }

    [FromQuery(Name = F13Constant.Url.Query.NumberOfRecord)]
    public int NumberOfRecord { get; set; }
}
