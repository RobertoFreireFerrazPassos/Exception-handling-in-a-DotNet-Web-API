namespace TasksAPI.DataContracts.Response;

public class Response
{
    public int StatusCode { get; }

    public Response(int statusCode)
    {
        StatusCode = statusCode;
    }
}
