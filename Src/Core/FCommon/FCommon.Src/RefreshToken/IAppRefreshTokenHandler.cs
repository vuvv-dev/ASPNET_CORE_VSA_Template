using FCommon.Src.Constants;

namespace FCommon.Src.RefreshToken;

public interface IAppRefreshTokenHandler
{
    string Generate(int tokenLength = AppConstants.REFRESH_TOKEN_LENGTH);
}
