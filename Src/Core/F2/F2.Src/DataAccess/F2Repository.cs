namespace F2.Src.DataAccess;

public sealed class F2Repository : IF2Repository
{
    public string GetUser()
    {
        const string Response = "This is user from database";

        return Response;
    }
}
