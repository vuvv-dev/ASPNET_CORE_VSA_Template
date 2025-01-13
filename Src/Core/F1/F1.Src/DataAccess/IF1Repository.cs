using System.Threading;
using System.Threading.Tasks;
using F1.Src.Models;

namespace F1.Src.DataAccess;

public interface IF1Repository
{
    Task<bool> IsUserFoundByEmailAsync(string email, CancellationToken ct);

    Task<F1PasswordSignInResultModel> CheckPasswordSignInAsync(
        string email,
        string password,
        CancellationToken ct
    );

    Task CreateRefreshTokenAsync(F1RefreshTokenModel model, CancellationToken ct);
}
