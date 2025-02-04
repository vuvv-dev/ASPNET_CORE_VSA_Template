using System.Text.Json.Serialization;

namespace F1.Presentation;

public sealed class Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
