using Microsoft.AspNetCore.Mvc;
using TasksAPI.Enum;
using TasksAPI.Extensions;
using TasksAPI.Services;

namespace TasksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(ITasksService tasksService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] int type)
    {
        if (!System.Enum.IsDefined(typeof(TaskTypeEnum), type))
        {
            throw new ArgumentException($"Invalid task type '{type}'");
        }

        return tasksService.GetTasks((TaskTypeEnum)type).ToHttpResponse();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Invalid id '{id}'");
        }

        return tasksService.GetTask(id).ToHttpResponse();
    }
}