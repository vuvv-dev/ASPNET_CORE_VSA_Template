using System.Text.Json.Serialization;

namespace F16.Presentation;

public sealed class F16Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
