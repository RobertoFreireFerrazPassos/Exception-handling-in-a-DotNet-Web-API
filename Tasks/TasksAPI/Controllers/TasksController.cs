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
        try
        {
            return Ok(tasksRepository.GetTasks((TaskTypeEnum)System.Enum.Parse(typeof(TaskTypeEnum), type, ignoreCase: true)));
        }
        catch (Exception ex) {
            return StatusCode(500);
        }
    }
}
