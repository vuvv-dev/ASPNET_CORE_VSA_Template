using FCommon.Constants;

namespace FCommon.RefreshToken;

public interface IAppRefreshTokenHandler
{
    string Generate(int tokenLength = AppConstant.REFRESH_TOKEN_LENGTH);
}
