using System.Threading;
using System.Threading.Tasks;
using F6.Models;

namespace F6.DataAccess;

public interface IRepository
{
    Task<RefreshTokenModel> DoesRefreshTokenBelongToAccessTokenAsync(
        string refreshTokenId,
        string refreshTokenValue,
        CancellationToken ct
    );

    Task<bool> UpdateRefreshTokenAsync(UpdateRefreshTokenModel model, CancellationToken ct);
}
