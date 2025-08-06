# Exception-handling-in-a-DotNet-Web-API

## TasksAPI example

### VALID -> GET api/Tasks/1

- Task found in database
- Controller will return Ok with SuccessfulResponse with data

```json
200
{
  "data": {
    "id": 1,
    "name": "Task 1",
    "type": 2
  },
  "statusCode": 200,
  "message": ""
}
```

### INVALID -> GET api/Tasks/0

- Throw new ArgumentException if id is invalid
- ErrorHandlerMiddleware will log stack trace exception and error message (to debug later) and return error response

```json
400
{
  "statusCode": 400,
  "message": "Invalid id '0'"
}
```

### NOT FOUND -> GET api/Tasks/123

- Task not found in database (Not an exception)
- Repository will return Result<TaskDto>.Failure($"Task with id '{id}' not found");
- Controller will return NotFound with error message

```json
404
{
  "statusCode": 404,
  "message": "Task with id '123' not found"
}
```

## Throw exception

When an exception is thrown, it immediately interrupts the normal flow of control in the program.

Note: Exceptions are for exceptional situations. Don't use to control program flow.

```cs
throw new ArgumentNullException("someMethod received a null argument!");
```

## Stack Trace

The stack trace begins at the statement where the exception is thrown and ends at the catch statement that catches the exception.

## Best Practices

- Don't raise exceptions from unexpected places. Some methods, such as Equals, GetHashCode, and ToString methods, static constructors, and equality operators, shouldn't throw exceptions
- Don't use exceptions to change the flow of a program as part of ordinary execution. Use exceptions to report and handle error conditions.
- Don't return exceptions. Thrown exception

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
- When your code can't recover from an exception, don't catch that exception. Enable methods further up the call stack to recover if possible.

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

### Finally Blocks

A finally block enables you to clean up actions that are performed in a try block

```cs
FileStream? file = null;
FileInfo fileinfo = new System.IO.FileInfo("./file.txt");
try
{
    file = fileinfo.OpenWrite();
    file.WriteByte(0xF);
}
finally
{
    // Check for null because OpenWrite might have failed.
    file?.Close();
}
```

### Clean up resources

Clean up resources that are allocated with either using statements or finally blocks:
- Prefer using statements to automatically clean up resources when exceptions are thrown. 
- Use finally blocks to clean up resources that don't implement IDisposable. Code in a finally clause is almost always executed even when exceptions are thrown.

### Best Practices

- When catching exceptions in C#, it’s important to catch more specific exceptions first and then catch more generic exceptions like Exception last. This ensures that specific issues are handled properly before falling back to a general catch-all block and then each catch block can handle a specific error type with the appropriate logic
- Don’t catch exceptions just to suppress them unless there’s a very specific reason.

## Async

- Throw argument validation exceptions synchronously. In task-returning methods, you should validate arguments and throw any corresponding exceptions, such as ArgumentException and ArgumentNullException, before entering the asynchronous part of the method. Exceptions that are thrown in the asynchronous part of the method are stored in the returned task and don't emerge until, for example, the task is awaited.
- It's better to catch OperationCanceledException instead of TaskCanceledException, which derives from OperationCanceledException, when you call an asynchronous method. Many asynchronous methods throw an OperationCanceledException exception if cancellation is requested. These exceptions enable execution to be efficiently halted and the callstack to be unwound once a cancellation request is observed.

## Handle common conditions to avoid exceptions

- For conditions that are likely to occur but might trigger an exception, consider handling them in a way that avoids the exception.
- Use exception handling if the event doesn't occur often, that is, if the event is truly exceptional and indicates an error
- Call Try* methods to avoid exceptions. Use TryParse instead of Parse.

## Restore state when methods don't complete due to exceptions

- Callers should be able to assume that there are no side effects when an exception is thrown from a method. For example, if you have code that transfers money by withdrawing from one account and depositing in another account, and an exception is thrown while executing the deposit, you don't want the withdrawal to remain in effect.
- The preceding method doesn't directly throw any exceptions. However, you must write the method so that the withdrawal is reversed if the deposit operation fails. One way to handle this situation is to catch any exceptions thrown by the deposit transaction and roll back the withdrawal.

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

- Handle Exceptions globally via Custom middleware to avoid duplication of try..catch blocks in all controller actions
- Add a custom middleware to catch unhandled exceptions
- Create a error response class for consistent error responses
- Map Exceptions to the appropriate HTTP status codes
- Return meaningful and secure error messages to clients

## Handling exceptions with logs

- Log exceptions in middleware

## Retry Logic and Transient Fault Handling

- Retry Logic and Transient Fault Handling
- Using Polly for retries, fallbacks, and circuit breakers to handle transient exceptions

Note: Transient exceptions are those that when retried could succeed without changing anything.

## Result Pattern

As we previously discussed, exception should be used for exceptional situations. If we are dealing with APIs, using the result pattern instead of throwing exceptions is indeed a more efficient and recommended approach. This pattern allows for better error handling and avoids the performance costs associated with exceptions.

### Benefits:

- Explicit Error Handling: the caller must handle the success and failure cases explicitly. From the method signature, it's obvious that an error may be returned.
- Improved Performance: reduces the overhead associated with exceptions.
- Better Testing: simplifies unit testing as it's much easier to mock Result Object than throwing and handling exceptions.
- Safety: a result object should contain information that can be exposed to the outside world. While you can save all the details using Logger or other tools.

### Drawbacks:

- Response: It is hard to map the Result object to the HttpStatusCode
- Multiple issues: Since the flow wasn't interrupted when the failure happen, it might have happened multiple issues, which http status code should we return?
- Verbosity: can introduce more code compared to using exceptions as you need to mark all methods in the stacktrace to return Result Object
- Not Suitable for All Cases: exceptions are still appropriate for truly exceptional situations that are not expected during normal operation.

## References:

https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/creating-and-throwing-exceptions

https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions

https://antondevtips.com/blog/how-to-replace-exceptions-with-result-pattern-in-dotnet?utm_source=linkedin&utm_medium=social&utm_campaign=05-05-2025


