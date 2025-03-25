using System.Threading;
using System.Threading.Tasks;
using F001.Models;

namespace F001.DataAccess;

public interface IRepository
{
    Task<bool> IsUserFoundByEmailAsync(string email, CancellationToken ct);

    Task<PasswordSignInResultModel> CheckPasswordSignInAsync(
        string email,
        string password,
        CancellationToken ct
    );

    Task<bool> CreateRefreshTokenAsync(RefreshTokenModel model, CancellationToken ct);
}
