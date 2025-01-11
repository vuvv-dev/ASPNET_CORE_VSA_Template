namespace F1.Src.Presentation;

public sealed class F1Response
{
    public int AppCode { get; set; }

    public BodyDto Body { get; set; }

    public sealed class BodyDto { }
}
