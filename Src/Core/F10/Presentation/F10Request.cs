using F10.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F10.Presentation;

[ValidateNever]
public sealed class F10Request
{
    [FromRoute]
    public long TodoTaskListId { get; set; }

    [FromQuery(Name = F10Constant.Url.Query.NumberOfRecord)]
    public int NumberOfRecord { get; set; }
}
