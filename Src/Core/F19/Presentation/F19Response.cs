using System.Text.Json.Serialization;

namespace F19.Presentation;

public sealed class F19Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
