using Microsoft.AspNetCore.Mvc;

namespace F2.Src.Presentation;

public sealed class F2Request
{
    [FromRoute]
    public long ListId { get; init; }
}
