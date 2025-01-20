using System.Text.Json.Serialization;

namespace F13.Presentation;

public sealed class F13Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
