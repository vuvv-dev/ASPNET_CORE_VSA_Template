namespace FCommon.Src.IdGeneration;

public interface IAppIdGenerator
{
    long NextId();

    AppDecodedIdModel DecodeId(long id);
}
