using System.Threading;
using System.Threading.Tasks;
using F6.Models;

namespace F6.DataAccess;

public interface IF6Repository
{
    Task<F6RefreshTokenModel> DoesRefreshTokenBelongToAccessTokenAsync(
        string refreshTokenId,
        string refreshTokenValue,
        CancellationToken ct
    );

    Task<bool> UpdateRefreshTokenAsync(F6UpdateRefreshTokenModel model, CancellationToken ct);
}
