using System;

namespace F13.Models;

public sealed class F13TodoTaskModel
{
    public long Id { get; set; }

    public string Content { get; set; }

    public DateTime DueDate { get; set; }

    public bool IsExpired { get; set; }

    public bool IsImportant { get; set; }

    public bool IsInMyDay { get; set; }

    public bool HasNote { get; set; }

    public bool HasSteps { get; set; }

    public bool IsRecurring { get; set; }
}
