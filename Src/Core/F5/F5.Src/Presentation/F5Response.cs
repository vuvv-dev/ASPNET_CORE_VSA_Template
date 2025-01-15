using System.Text.Json.Serialization;

namespace F5.Src.Presentation;

public sealed class F5Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel { }
}
