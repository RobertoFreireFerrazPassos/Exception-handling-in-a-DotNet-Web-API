namespace TasksAPI.DataContracts.Response;

public class SuccessfulResponse<T> : Response
{
    public T Data { get; }

    public SuccessfulResponse(int statusCode, T value) : base(statusCode, string.Empty)
    {
        Data = value;
    }
}
