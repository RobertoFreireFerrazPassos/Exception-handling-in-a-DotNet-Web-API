namespace TasksAPI.DataContracts.Response;

public class ErrorResponse : Response
{
    public string? Error { get; }

    public ErrorResponse(int statusCode, string error) : base(statusCode)
    {
        Error = error;
    }
}
