using System.Text.Json.Serialization;

namespace F7.Src.Presentation;

public sealed class F7Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
