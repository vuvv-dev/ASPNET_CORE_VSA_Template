using System.Text.Json.Serialization;

namespace F4.Src.Presentation;

public sealed class F4Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public string JWSToken { get; set; }
    }
}
