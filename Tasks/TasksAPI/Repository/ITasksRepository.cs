using TasksAPI.Dtos;
using TasksAPI.Enum;

namespace TasksAPI.Repository;

public interface ITasksRepository
{
    List<TaskDto> GetTasks(TaskTypeEnum type);
}
