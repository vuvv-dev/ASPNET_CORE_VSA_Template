using System;

namespace F2.Src.Presentation;

public sealed class F2Response
{
    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto
    {
        public TodoTaskListDto TodoTaskList { get; set; }

        public sealed class TodoTaskListDto
        {
            public long Id { get; set; }

            public string Name { get; set; }

            public DateTime CreatedDate { get; set; }

            public long UserId { get; set; }
        }
    }
}
