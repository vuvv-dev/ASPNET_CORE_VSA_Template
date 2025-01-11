using System;
using SnowflakeGenerator;
using SnowflakeGenerator.Exceptions;

namespace FA3.Src.Generator;

public sealed class SnowFlakeGenerator : IIdGenerator
{
    private static readonly Settings GeneratorSettings = new()
    {
        CustomEpoch = new(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
    };

    private static readonly Snowflake Generator = new(GeneratorSettings);

    public long NextId()
    {
        try
        {
            return Generator.NextID();
        }
        catch (SnowflakeException)
        {
            return -1;
        }
    }
}
