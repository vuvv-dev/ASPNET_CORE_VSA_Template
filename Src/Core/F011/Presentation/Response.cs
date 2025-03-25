using System.Text.Json.Serialization;

namespace F011.Presentation;

public sealed class Response
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
