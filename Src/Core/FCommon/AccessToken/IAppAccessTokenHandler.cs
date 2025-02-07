using System.Collections.Generic;
using System.Security.Claims;

namespace FCommon.AccessToken;

public interface IAppAccessTokenHandler
{
    string GenerateJWT(IEnumerable<Claim> claims, int additionalMinutesFromNow);
}
