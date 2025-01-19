using System;

namespace FCommon.IdGeneration;

public sealed class AppDecodedIdModel
{
    public uint MachineId { get; set; }

    public DateTimeOffset CreatedTimestamp { get; set; }

    public long Sequence { get; set; }
}
