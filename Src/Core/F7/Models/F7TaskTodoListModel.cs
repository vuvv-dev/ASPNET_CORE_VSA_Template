using System;

namespace F7.Models;

public class F7TaskTodoListModel
{
    public long Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public long UserId { get; set; }
}
