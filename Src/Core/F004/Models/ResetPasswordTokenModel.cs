using System;

namespace F004.Models;

public sealed class ResetPasswordTokenModel
{
    public string LoginProvider { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public DateTime ExpiredAt { get; set; }

    public long UserId { get; set; }
}
