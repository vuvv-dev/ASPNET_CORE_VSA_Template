using System.Threading;
using System.Threading.Tasks;

namespace F5.Src.DataAccess;

public interface IF5Repository
{
    Task<bool> ResetPasswordAsync(
        long userId,
        string password,
        string passwordResetToken,
        string passwordResetTokenId,
        CancellationToken ct
    );

    Task<bool> IsPasswordValidAsync(string password, CancellationToken ct);

    Task<string> GetResetPasswordTokenByIdAsync(string resetPasswordTokenId, CancellationToken ct);
}
