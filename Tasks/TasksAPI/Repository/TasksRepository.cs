using TasksAPI.Dtos;
using TasksAPI.Enum;
using TasksAPI.Model;

namespace TasksAPI.Repository;

public class TasksRepository : ITasksRepository
{

    private readonly List<TaskDto> _tasks = new List<TaskDto>()
    {
        new TaskDto { Id = 1, Name = "Task 1", Type = TaskTypeEnum.Generic },
        new TaskDto { Id = 2, Name = "Basic Task 1", Type = TaskTypeEnum.Basic },
        new TaskDto { Id = 3, Name = "Basic Task 2", Type = TaskTypeEnum.Basic },
        new TaskDto { Id = 4, Name = "Default Task 1", Type = TaskTypeEnum.Default },
        new TaskDto { Id = 5, Name = "Default Task 2", Type = TaskTypeEnum.Default }
    };

    public Result<TaskDto> GetTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task is null)
        {
            return Result<TaskDto>.Failure($"Task with id '{id}' not found", ErrorTypeEnum.NotFound);
        }

        return Result<TaskDto>.Success(task);
    }

    public Result<List<TaskDto>> GetTasks(TaskTypeEnum type)
    {
        var tasks = _tasks.Where(t => t.Type == type).ToList();

        return Result<List<TaskDto>>.Success(tasks);
    }
}
