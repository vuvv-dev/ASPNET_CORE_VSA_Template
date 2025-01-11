namespace FA2.Src.RefreshToken;

public interface IAppRefreshTokenHandler
{
    string Generate(int tokenLength = 15);
}
