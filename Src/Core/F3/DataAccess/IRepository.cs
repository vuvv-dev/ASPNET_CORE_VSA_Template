using System.Threading;
using System.Threading.Tasks;
using F3.Models;

namespace F3.DataAccess;

public interface IRepository
{
    Task<bool> DoesEmailExistsAsync(string email, CancellationToken ct);

    Task<bool> IsPasswordValidAsync(string email, string password, CancellationToken ct);

    Task<bool> CreateUserAsync(UserInfoModel user, CancellationToken ct);
}
