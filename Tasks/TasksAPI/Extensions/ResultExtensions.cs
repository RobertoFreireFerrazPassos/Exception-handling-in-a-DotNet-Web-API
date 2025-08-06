using Microsoft.AspNetCore.Mvc;
using TasksAPI.Enum;
using TasksAPI.Model;

namespace TasksAPI.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToHttpResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        

        return result.ErrorType switch
        {
            ErrorTypeEnum.NotFound => new ObjectResult(new ProblemDetails
            {
                Title = "Resource Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = result.ErrorMessage
            })
            {
                StatusCode = StatusCodes.Status404NotFound
            },

            ErrorTypeEnum.Unauthorized => new ObjectResult(new ProblemDetails
            {
                Title = "Unauthorized Access",
                Status = StatusCodes.Status401Unauthorized,
                Detail = result.ErrorMessage
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            },

            ErrorTypeEnum.Forbidden => new ObjectResult(new ProblemDetails
            {
                Title = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Detail = result.ErrorMessage
            })
            {
                StatusCode = StatusCodes.Status403Forbidden
            },

            ErrorTypeEnum.Conflict => new ObjectResult(new ProblemDetails
            {
                Title = "Conflict Occurred",
                Status = StatusCodes.Status409Conflict,
                Detail = result.ErrorMessage
            })
            {
                StatusCode = StatusCodes.Status409Conflict
            },

            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Detail = result.ErrorMessage
            })
        };
    }
}
