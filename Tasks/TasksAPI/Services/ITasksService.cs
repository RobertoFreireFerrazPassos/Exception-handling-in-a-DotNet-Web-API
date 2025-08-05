using TasksAPI.Dtos;
using TasksAPI.Enum;
using TasksAPI.Model;

namespace TasksAPI.Services;

public interface ITasksService
{
    Result<List<TaskDto>> GetTasks(TaskTypeEnum type);
}
