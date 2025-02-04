namespace F6.Presentation.Filters.SetStateBag;

public sealed class StateBag
{
    public Request HttpRequest { get; set; }

    public Response HttpResponse { get; set; }

    public long AccessTokenId { get; set; }

    public long UserId { get; set; }
}
