# Exception-handling-in-a-DotNet-Web-API


## Basic Try-Catch-Finally

The response in this case is: ArgumentNullExceptionFinally:

- Result += "Okay"; is not executed
- Catch ArgumentNullException, but not Exception
- result += "Finally"; is always executed UNLESS a return statement breaks the flow

```cs
var result = string.Empty;
try
{
    //  Block of code to try
    throw new ArgumentNullException("test");
    result += "Okay";
}
catch (ArgumentNullException e)
{
    //  Block of code to handle exception
    result += "ArgumentNullException";
}
catch (Exception e)
{
    //  Block of code to handle exception
    result += "Catch";
}
finally
{
    // The finally statement is always executed UNLESS a return statement breaks the flow
    result += "Finally";
}

return Ok(result);
```

## throw; vs throw ex;

The **throw;** statement is used to rethrow the current exception while preserving the original stack trace. This is important because the stack trace provides valuable information about where the exception originally occurred, making it easier to debug and diagnose issues

Scenario:

Consider a situation where a method deep in the call stack throws an exception, and you catch it in a higher-level method to perform some logging or cleanup

```cs
try
{
  // some code
}
catch
{
  throw;
}
```

On the other hand, **throw ex;** resets the stack trace to the point where the throw ex statement is executed, losing the original context. This means that the original stack trace information is lost, which can obscure the source of the error and make debugging more difficult.

```cs
try
{
  // some code
}
catch
{
  throw ex;
}
```

## Custom Exception Types

Custom Exception Types. Creating and using custom exception classes (e.g., NotFoundException). Mapping custom exceptions to appropriate HTTP status codes

## Custom middleware to catch unhandled exceptions

https://localhost:7052/api/Tasks/tasks/weqw

(500 for unhandled server errors, 400 for bad request, 401/403 for unauthorized/forbidden, 404 for not found). Using ProblemDetails for consistent error responses. https://www.treinaweb.com.br/blog/tratando-erros-em-uma-api-asp-net-core-com-middleware

## Handling exceptions with logs

Log issue. Serilog. Logging in middleware and filters

## When to use Exception

- Exception like the name say is a exception. Don’t use as a feature or a condition in the flow

Not found in database. Null instead of exception and Handle issue in upper logic.

Exception Handling in External Dependencies. HTTP clients, databases, file I/O, etc. Wrapping external service errors in custom exceptions

## Retry Logic and Transient Fault Handling

Retry Logic and Transient Fault Handling. Using Polly for retries, fallbacks, and circuit breakers. Avoiding overuse in non-transient exceptions

## Result Pattern

Result Pattern. https://antondevtips.com/blog/how-to-replace-exceptions-with-result-pattern-in-dotnet?utm_source=linkedin&utm_medium=social&utm_campaign=05-05-2025

## Best Practices

- Use **throw;** instead of "throw ex;" in most situations to rethrow the current exception while preserving the original stack trace
- Handle Exceptions globally via Custom middleware to avoid duplication of try..catch blocks in all controller actions.

Look at Microsoft best practices for exception. https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions
Best Practices & Anti-patterns. Avoid empty catch blocks. Don't expose internal exception messages. Return meaningful and secure error messages to clients


