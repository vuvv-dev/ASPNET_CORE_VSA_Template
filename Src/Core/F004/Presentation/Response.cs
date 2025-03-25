using System.Text.Json.Serialization;

namespace F004.Presentation;

public sealed class Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public string ResetPasswordToken { get; set; }
    }
}
