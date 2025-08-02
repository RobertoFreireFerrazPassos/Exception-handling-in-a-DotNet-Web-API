# Exception-handling-in-a-DotNet-Web-API

## Throw exception

You can throw a new Exception in the code.
When an exception is thrown, it immediately interrupts the normal flow of control in the program.

```cs
throw new ArgumentNullException("someMethod received a null argument!");
```

## Best Practices

- Don't raise exceptions from unexpected places. Some methods, such as Equals, GetHashCode, and ToString methods, static constructors, and equality operators, shouldn't throw exceptions
- Throw argument validation exceptions synchronously. In task-returning methods, you should validate arguments and throw any corresponding exceptions, such as ArgumentException and ArgumentNullException, before entering the asynchronous part of the method. Exceptions that are thrown in the asynchronous part of the method are stored in the returned task and don't emerge until, for example, the task is awaited. For more information, see Exceptions in task-returning methods.
- Don't use exceptions to change the flow of a program as part of ordinary execution. Use exceptions to report and handle error conditions.
- Exceptions shouldn't be returned as a return value or parameter instead of being thrown.

## When to throw an exception

Programmers should throw exceptions when one or more of the following conditions are true:

- The method can't complete its defined functionality. For example, if a parameter to a method has an invalid value
- An inappropriate call to an object is made, based on the object state. One example might be trying to write to a read-only file
- When an argument to a method causes an exception. The original exception should be passed as the InnerException parameter to the new exception

## throw; vs throw ex;

The **throw;** statement is used to rethrow the current exception while preserving the original stack trace. This is important because the stack trace provides valuable information about where the exception originally occurred, making it easier to debug and diagnose issues

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

### Best Practices

- Use **throw;** instead of "throw ex;" in most situations to rethrow the current exception while preserving the original stack trace
- Don't raise exceptions in finally clauses
- When a method deep in the call stack throws an exception, catch it in a higher-level method to perform some logging or cleanup.

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
### Best Practices

- When catching exceptions in C#, it’s important to catch more specific exceptions first and then catch more generic exceptions like Exception last. This ensures that specific issues are handled properly before falling back to a general catch-all block.
- Each catch block can handle a specific error type with the appropriate logic
-  Don’t catch exceptions just to suppress them unless there’s a very specific reason. Always handle exceptions properly by logging them or taking necessary corrective actions.

## Custom Exception Types

- .NET provides a hierarchy of exception classes ultimately derived from the Exception base class. For example, ArgumentException. However, if none of the predefined exceptions meet your needs, you can create your own exception class by deriving from the Exception class.
- An inner exception describes the error that caused the current exception.

### Best Practices

- End exception class names with Exception and derive it from the Exception class
- Include three constructors. the parameterless constructor, a constructor that takes a string message, and a constructor that takes a string message and an inner exception.
- Provide additional properties only when there's a programmatic scenario where the additional information is useful. For example, the FileNotFoundException provides the FileName property.
- Include a localized string message. Avoid constant string for example "throw new StudentNotFoundException("The student cannot be found.", "John");"
- Introduce a new exception class only when a predefined one doesn't apply.

## Custom middleware to catch unhandled exceptions

https://localhost:7052/api/Tasks/tasks/weqw

(500 for unhandled server errors, 400 for bad request, 401/403 for unauthorized/forbidden, 404 for not found). Using ProblemDetails for consistent error responses. https://www.treinaweb.com.br/blog/tratando-erros-em-uma-api-asp-net-core-com-middleware

### Best Practices

- Handle Exceptions globally via Custom middleware to avoid duplication of try..catch blocks in all controller actions.
- Map Exceptions to the appropriate HTTP status codes
- Return meaningful and secure error messages to clients

## Handling exceptions with logs

Log issue. Serilog. Logging in middleware and filters

## Retry Logic and Transient Fault Handling

Retry Logic and Transient Fault Handling. Using Polly for retries, fallbacks, and circuit breakers. Avoiding overuse in non-transient exceptions

## Result Pattern

If we are dealing with APIs, using the result pattern instead of throwing exceptions is indeed a more efficient and recommended approach. This pattern allows for better error handling and avoids the performance costs associated with exceptions.

Result Pattern. https://antondevtips.com/blog/how-to-replace-exceptions-with-result-pattern-in-dotnet?utm_source=linkedin&utm_medium=social&utm_campaign=05-05-2025

## Best Practices

Look at Microsoft best practices for exception. https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions


