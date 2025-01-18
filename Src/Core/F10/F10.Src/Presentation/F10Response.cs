using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace F10.Src.Presentation;

public sealed class F10Response
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public IEnumerable<TodoTaskListDto> TodoTaskLists { get; set; }

        public long NextCursor { get; set; }

        public sealed class TodoTaskListDto
        {
            public long Id { get; set; }

            public string Name { get; set; }
        }
    }
}
