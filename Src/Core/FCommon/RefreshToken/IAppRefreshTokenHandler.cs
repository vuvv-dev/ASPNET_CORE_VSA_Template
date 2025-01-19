using FCommon.Constants;

namespace FCommon.RefreshToken;

public interface IAppRefreshTokenHandler
{
    string Generate(int tokenLength = AppConstants.REFRESH_TOKEN_LENGTH);
}
