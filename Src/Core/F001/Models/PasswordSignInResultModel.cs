namespace F001.Models;

public sealed class PasswordSignInResultModel
{
    public bool IsSuccessful { get; set; }

    public bool IsLockedOut { get; set; }

    public long UserId { get; set; }
}
