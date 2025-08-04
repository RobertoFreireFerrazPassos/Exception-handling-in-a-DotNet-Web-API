using Microsoft.AspNetCore.Mvc;
using TasksAPI.DataContracts.Response;
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
        System.Enum.TryParse(typeof(TaskTypeEnum), type, ignoreCase: true, out var taskType);

        if (taskType is null)
        {
            throw new ArgumentException($"Invalid task type '{type}'");
        }

        var tasks = tasksRepository.GetTasks((TaskTypeEnum) taskType);
        return Ok(new TaskResponse() { Tasks = tasks });
    }
}
