using Newtonsoft.Json;
using System.Net;
using TasksAPI.DataContracts.Response;

namespace TasksAPI.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _log;

    public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory log)
    {
        this._next = next;
        this._log = log.CreateLogger("MyErrorHandler");
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);            
        }
        catch (ArgumentException ex)
        {
            var error = new ErrorResponse((int)HttpStatusCode.BadRequest, ex.Message);
            await HandleErrorAsync(httpContext, error, ex);
        }
        catch (Exception ex)
        {
            var error = new ErrorResponse((int)HttpStatusCode.InternalServerError, "Internal server error");
            await HandleErrorAsync(httpContext, error, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, ErrorResponse errorResponse, Exception exception)
    {
        _log.LogError($"Error: {exception.Message}");
        _log.LogError($"Stack: {exception.StackTrace}");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorResponse.StatusCode;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    }
}
