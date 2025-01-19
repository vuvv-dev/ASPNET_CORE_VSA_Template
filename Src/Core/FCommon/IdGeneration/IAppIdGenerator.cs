namespace FCommon.IdGeneration;

public interface IAppIdGenerator
{
    long NextId();

    AppDecodedIdModel DecodeId(long id);
}
