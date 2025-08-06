using TasksAPI.Enum;

namespace TasksAPI.Model;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string? ErrorMessage { get; }
    public ErrorTypeEnum? ErrorType { get; }

    private Result(bool isSuccess, T? value, string? error, ErrorTypeEnum? errorType)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = error;
        ErrorType = errorType;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null, null);
    }

    public static Result<T> Failure(string message, ErrorTypeEnum type)
    {
        return new Result<T>(false, default(T), message, type);
    }
}