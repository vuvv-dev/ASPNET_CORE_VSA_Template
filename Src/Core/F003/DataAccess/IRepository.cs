using System.Threading;
using System.Threading.Tasks;
using F003.Models;

namespace F003.DataAccess;

public interface IRepository
{
    Task<bool> DoesEmailExistsAsync(string email, CancellationToken ct);

    Task<bool> IsPasswordValidAsync(string email, string password, CancellationToken ct);

    Task<bool> CreateUserAsync(UserInfoModel user, CancellationToken ct);
}
