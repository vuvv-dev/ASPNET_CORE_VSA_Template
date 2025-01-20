using System.Text.Json.Serialization;

namespace F11.Presentation;

public sealed class F11Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public long TodoTaskId { get; set; }
    }
}
