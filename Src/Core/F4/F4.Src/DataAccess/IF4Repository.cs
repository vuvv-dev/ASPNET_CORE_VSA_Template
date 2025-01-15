using System.Threading;
using System.Threading.Tasks;
using F4.Src.Models;

namespace F4.Src.DataAccess;

public interface IF4Repository
{
    Task<bool> CreateResetPasswordTokenAsync(F4ResetPasswordTokenModel model, CancellationToken ct);

    Task<long> GetUserIdAsync(string email, CancellationToken ct);

    Task<string> GeneratePasswordResetTokenAsync(long userId, CancellationToken ct);
}
