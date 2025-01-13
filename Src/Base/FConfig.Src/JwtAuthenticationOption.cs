using System.Collections.Generic;

namespace FConfig.Src;

public sealed class JwtAuthenticationOption
{
    public bool ValidateIssuer { get; init; }

    public bool ValidateAudience { get; init; }

    public bool ValidateLifetime { get; init; }

    public bool ValidateIssuerSigningKey { get; init; }

    public bool RequireExpirationTime { get; init; }

    public string ValidIssuer { get; init; }

    public string ValidAudience { get; init; }

    public string IssuerSigningKey { get; init; }

    public IEnumerable<string> ValidTypes { get; init; } = [];
}
