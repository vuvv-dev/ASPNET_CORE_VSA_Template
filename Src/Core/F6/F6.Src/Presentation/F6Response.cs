using System.Text.Json.Serialization;

namespace F6.Src.Presentation;

public sealed class F6Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
