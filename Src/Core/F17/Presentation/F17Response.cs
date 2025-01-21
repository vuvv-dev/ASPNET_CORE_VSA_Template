using System.Text.Json.Serialization;

namespace F17.Presentation;

public sealed class F17Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
