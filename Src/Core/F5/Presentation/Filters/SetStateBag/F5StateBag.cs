namespace F5.Presentation.Filters.SetStateBag;

public sealed class F5StateBag
{
    public F5Request HttpRequest { get; set; }

    public F5Response HttpResponse { get; set; }

    public long ResetPasswordTokenId { get; set; }

    public long UserId { get; set; }
}
