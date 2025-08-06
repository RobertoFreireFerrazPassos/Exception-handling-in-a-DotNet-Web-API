namespace TasksAPI.DataContracts.Response;

public class Response
{
    public int StatusCode { get; }

    public string Message { get; }

    public Response(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}
