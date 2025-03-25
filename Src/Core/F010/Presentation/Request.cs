using F010.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F010.Presentation;

[ValidateNever]
public sealed class Request
{
    [FromRoute]
    public long TodoTaskListId { get; set; }

    [FromQuery(Name = Constant.Url.Query.NumberOfRecord)]
    public int NumberOfRecord { get; set; }
}
