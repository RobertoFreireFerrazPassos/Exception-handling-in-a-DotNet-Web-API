using TasksAPI.Dtos;
using TasksAPI.Enum;
using TasksAPI.Model;

namespace TasksAPI.Repository;

public interface ITasksRepository
{
    Result<List<TaskDto>> GetTasks(TaskTypeEnum type);

    Result<TaskDto> GetTask(int id);
}
