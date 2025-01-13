namespace F1.Src.Models;

public sealed class F1CheckPasswordSignInResultModel
{
    public bool IsSuccessful { get; set; }

    public bool IsLockedOut { get; set; }

    public long UserId { get; set; }
}
