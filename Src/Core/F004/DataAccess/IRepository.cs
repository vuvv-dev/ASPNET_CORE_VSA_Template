using System.Threading;
using System.Threading.Tasks;
using F004.Models;

namespace F004.DataAccess;

public interface IRepository
{
    Task<bool> CreateResetPasswordTokenAsync(ResetPasswordTokenModel model, CancellationToken ct);

    Task<long> GetUserIdAsync(string email, CancellationToken ct);

    Task<string> GeneratePasswordResetTokenAsync(long userId, CancellationToken ct);
}
