using System.Collections.Generic;
using System.Security.Claims;

namespace FCommon.Src.AccessToken;

public interface IAppAccessTokenHandler
{
    string GenerateJWS(IEnumerable<Claim> claims, int additionalMinutesFromNow);
}
