namespace F5.Presentation.Filters.SetStateBag;

public sealed class StateBag
{
    public Request HttpRequest { get; set; }

    public Response HttpResponse { get; set; }

    public long ResetPasswordTokenId { get; set; }

    public long UserId { get; set; }
}
