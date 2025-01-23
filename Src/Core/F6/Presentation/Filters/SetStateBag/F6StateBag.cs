namespace F6.Presentation.Filters.SetStateBag;

public sealed class F6StateBag
{
    public F6Request HttpRequest { get; set; }

    public F6Response HttpResponse { get; set; }

    public long AccessTokenId { get; set; }

    public long UserId { get; set; }
}
