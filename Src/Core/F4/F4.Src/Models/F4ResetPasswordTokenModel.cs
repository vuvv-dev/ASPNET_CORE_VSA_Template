using System;

namespace F4.Src.Models;

public sealed class F4ResetPasswordTokenModel
{
    public string LoginProvider { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public DateTime ExpiredAt { get; set; }

    public long UserId { get; set; }
}
