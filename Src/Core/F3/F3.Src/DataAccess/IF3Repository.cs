using System.Threading;
using System.Threading.Tasks;
using F3.Src.Models;

namespace F3.Src.DataAccess;

public interface IF3Repository
{
    Task<bool> DoesEmailExistsAsync(string email, CancellationToken ct);

    Task<bool> IsPasswordValidAsync(string email, string password, CancellationToken ct);

    Task<bool> CreateUserAsync(F3UserInfoModel user, CancellationToken ct);
}
