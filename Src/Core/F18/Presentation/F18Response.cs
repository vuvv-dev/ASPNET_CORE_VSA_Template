using System.Text.Json.Serialization;

namespace F18.Presentation;

public sealed class F18Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
