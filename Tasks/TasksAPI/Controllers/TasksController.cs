using Microsoft.AspNetCore.Mvc;
using TasksAPI.Enum;
using TasksAPI.Repository;

namespace TasksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(ITasksRepository tasksRepository) : Controller
{
    [HttpGet("tasks/{type}")]
    public async Task<IActionResult> GetTasks(string type)
    {
        var result = string.Empty;
        try
        {
            //  Block of code to try
            throw new ArgumentNullException("test");
            result += "Okay";
        }
        catch (ArgumentNullException e)
        {
            //  Block of code to handle errors
            result += "ArgumentNullException";
            return Ok(result);
        }
        catch (Exception e)
        {
            //  Block of code to handle errors
            result += "Catch";
        }
        finally
        {
            result += "Finally";
        }

        return Ok(result);
        return Ok(tasksRepository.GetTasks((TaskTypeEnum)System.Enum.Parse(typeof(TaskTypeEnum), type, ignoreCase: true)));
    }
}
