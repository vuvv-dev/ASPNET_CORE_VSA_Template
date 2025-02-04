using System.Text.Json.Serialization;

namespace F2.Presentation;

public sealed class Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public TodoTaskListDto TodoTaskList { get; set; }

        public sealed class TodoTaskListDto
        {
            public long Id { get; set; }

            public string Name { get; set; }
        }
    }
}
