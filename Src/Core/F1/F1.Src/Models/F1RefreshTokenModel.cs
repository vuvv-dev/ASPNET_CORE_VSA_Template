using System;

namespace F1.Src.Models;

public sealed class F1RefreshTokenModel
{
    public string LoginProvider { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public DateTime ExpiredAt { get; set; }

    public long UserId { get; set; }
}
