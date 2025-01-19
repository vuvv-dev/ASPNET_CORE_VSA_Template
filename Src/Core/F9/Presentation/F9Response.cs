using System.Text.Json.Serialization;

namespace F9.Presentation;

public sealed class F9Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
