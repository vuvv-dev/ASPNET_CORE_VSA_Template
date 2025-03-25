using System;

namespace F001.Models;

public sealed class RefreshTokenModel
{
    public string LoginProvider { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public DateTime ExpiredAt { get; set; }

    public long UserId { get; set; }
}
