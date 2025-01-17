using System.Threading;
using System.Threading.Tasks;
using F6.Src.Models;

namespace F6.Src.DataAccess;

public interface IF6Repository
{
    Task<F6RefreshTokenModel> DoesRefreshTokenBelongToAccessTokenAsync(
        string refreshTokenId,
        string refreshTokenValue,
        CancellationToken ct
    );

    Task<bool> UpdateRefreshTokenAsync(F6UpdateRefreshTokenModel model, CancellationToken ct);
}
