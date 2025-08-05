using Microsoft.AspNetCore.Mvc;
using System.Net;
using TasksAPI.DataContracts.Response;
using TasksAPI.Dtos;
using TasksAPI.Enum;
using TasksAPI.Services;

namespace TasksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(ITasksService tasksService) : Controller
{
    [HttpGet("tasks/{type}")]
    public async Task<IActionResult> GetTasks(int type)
    {
        if (!System.Enum.IsDefined(typeof(TaskTypeEnum), type))
        {
            throw new ArgumentException($"Invalid task type '{type}'");
        }

        var taskResponse = tasksService.GetTasks((TaskTypeEnum)type);

        return taskResponse.IsSuccess ? 
            Ok(new SuccessfulResponse<List<TaskDto>>((int)HttpStatusCode.OK, taskResponse.Value!)) : 
            Conflict(new ErrorResponse((int)HttpStatusCode.Conflict, taskResponse.Error!));
    }
}