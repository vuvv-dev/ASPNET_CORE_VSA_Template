using System;

namespace F15.Models;

public sealed class F15TodoTaskModel
{
    public long Id { get; set; }

    public string Content { get; set; }

    public DateTime DueDate { get; set; }

    public bool IsInMyDay { get; set; }

    public bool IsImportant { get; set; }

    public string Note { get; set; }

    public bool IsFinished { get; set; }
}
