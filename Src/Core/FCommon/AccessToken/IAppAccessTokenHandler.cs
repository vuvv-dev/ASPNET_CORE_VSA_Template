using System.Collections.Generic;
using System.Security.Claims;

namespace FCommon.AccessToken;

public interface IAppAccessTokenHandler
{
    string GenerateJWS(IEnumerable<Claim> claims, int additionalMinutesFromNow);
}
