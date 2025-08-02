# Exception-handling-in-a-DotNet-Web-API

- Create a basic WebApi with controllers, service and database
- Implement Basic Try-Catch Blocks (Syntax and usage in controller actions, Catching specific exception types (e.g., ArgumentNullException, SqlException) and Re-throwing exceptions (throw; vs throw ex;))
- Custom middleware to catch unhandled exceptions. middleware (500 for unhandled server errors, 400 for bad request, 401/403 for unauthorized/forbidden, 404 for not found). Using ProblemDetails for consistent error responses. https://www.treinaweb.com.br/blog/tratando-erros-em-uma-api-asp-net-core-com-middleware
- Not found in database. Null instead of exception and Handle issue in upper logic.
- Custom Exception Types. Creating and using custom exception classes (e.g., NotFoundException). Mapping custom exceptions to appropriate HTTP status codes
- Look at Microsoft best practices for exception. https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions
- Best Practices & Anti-patterns. Avoid empty catch blocks. Don't expose internal exception messages. Return meaningful and secure error messages to clients
- Don't loose stack trace.
- Log issue. Serilog. Logging in middleware and filters
- Exception like the name say is a exception. Don’t use as a feature or a condition in the flow
- Result Pattern. https://antondevtips.com/blog/how-to-replace-exceptions-with-result-pattern-in-dotnet?utm_source=linkedin&utm_medium=social&utm_campaign=05-05-2025
- Retry Logic and Transient Fault Handling. Using Polly for retries, fallbacks, and circuit breakers. Avoiding overuse in non-transient exceptions
- Exception Handling in External Dependencies. HTTP clients, databases, file I/O, etc. Wrapping external service errors in custom exceptions



