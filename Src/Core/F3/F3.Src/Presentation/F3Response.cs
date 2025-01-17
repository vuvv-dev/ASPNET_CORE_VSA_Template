using System.Text.Json.Serialization;

namespace F3.Src.Presentation;

public sealed class F3Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
