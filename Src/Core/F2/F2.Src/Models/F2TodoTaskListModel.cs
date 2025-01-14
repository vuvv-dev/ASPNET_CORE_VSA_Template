using System;
using System.Collections.Generic;

namespace F2.Src.Models;

public sealed class F2TodoTaskListModel
{
    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public IEnumerable<TodoTaskModel> TodoTasks { get; set; }

    public sealed class TodoTaskModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime DueDate { get; set; }
    }
}
