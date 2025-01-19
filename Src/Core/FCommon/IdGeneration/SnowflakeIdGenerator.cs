using System;
using FConfig;
using Microsoft.Extensions.Configuration;
using SnowflakeGenerator;
using SnowflakeGenerator.Exceptions;

namespace FCommon.IdGeneration;

public sealed class SnowflakeIdGenerator : IAppIdGenerator
{
    private readonly Settings _idGeneratorSettings;
    private readonly Snowflake _idGenerator;

    public SnowflakeIdGenerator(IConfiguration configuration)
    {
        var option = configuration.GetRequiredSection("SnowflakeId").Get<SnowflakeIdOption>();

        _idGeneratorSettings = new()
        {
            MachineID = option.MachineId,
            CustomEpoch = new(
                option.CustomEpoch.Year,
                option.CustomEpoch.Month,
                option.CustomEpoch.Day,
                option.CustomEpoch.Hour,
                option.CustomEpoch.Minute,
                option.CustomEpoch.Second,
                TimeSpan.Zero
            ),
            MachineIDBitLength = option.MachineIDBitLength,
            SequenceBitLength = option.SequenceBitLength,
        };

        _idGenerator = new(_idGeneratorSettings);
    }

    public AppDecodedIdModel DecodeId(long id)
    {
        try
        {
            var (timestamp, machineID, sequence) = _idGenerator.DecodeID(id);

            long createdUnixTimestamp =
                timestamp + _idGeneratorSettings.CustomEpoch.Value.ToUnixTimeMilliseconds();

            var createdDateTime = DateTimeOffset.FromUnixTimeMilliseconds(createdUnixTimestamp);

            return new()
            {
                MachineId = machineID,
                Sequence = sequence,
                CreatedTimestamp = createdDateTime,
            };
        }
        catch (SnowflakeException)
        {
            return null;
        }
    }

    public long NextId()
    {
        try
        {
            return _idGenerator.NextID();
        }
        catch (SnowflakeException)
        {
            throw;
        }
    }
}
