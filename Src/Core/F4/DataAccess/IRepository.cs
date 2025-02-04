using System.Threading;
using System.Threading.Tasks;
using F4.Models;

namespace F4.DataAccess;

public interface IRepository
{
    Task<bool> CreateResetPasswordTokenAsync(ResetPasswordTokenModel model, CancellationToken ct);

    Task<long> GetUserIdAsync(string email, CancellationToken ct);

    Task<string> GeneratePasswordResetTokenAsync(long userId, CancellationToken ct);
}
