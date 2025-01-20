using System.Text.Json.Serialization;

namespace F12.Presentation;

public sealed class F12Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
