using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

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
            var error = new ProblemDetails
            {
                Title = ex.Message,
                Status = (int)HttpStatusCode.BadRequest,
            };
            await HandleErrorAsync(httpContext, error, ex);
        }
        catch (Exception ex)
        {
            var error = new ProblemDetails
            {
                Title = "Internal server error",
                Status = (int)HttpStatusCode.InternalServerError,
            };
            await HandleErrorAsync(httpContext, error, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, ProblemDetails problem, Exception exception)
    {
        _log.LogError($"Error: {exception.Message}");
        _log.LogError($"Stack: {exception.StackTrace}");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)problem.Status;
        await context.Response.WriteAsJsonAsync(problem);
    }
}
