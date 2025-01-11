using System.Collections.Generic;
using System.Security.Claims;

namespace FA2.Src.AccessToken;

public interface IAppAccessTokenHandler
{
    string GenerateJWS(IEnumerable<Claim> claims);
}
