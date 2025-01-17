using System.Text.Json.Serialization;

namespace F4.Src.Presentation;

public sealed class F4Response
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
