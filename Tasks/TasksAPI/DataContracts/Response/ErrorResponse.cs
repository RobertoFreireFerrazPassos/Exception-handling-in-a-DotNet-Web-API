namespace TasksAPI.DataContracts.Response;

public class ErrorResponse : Response
{
    public ErrorResponse(int statusCode, string error) : base(statusCode, error)
    {
    }
}
