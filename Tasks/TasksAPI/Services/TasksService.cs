using TasksAPI.Dtos;
using TasksAPI.Enum;
using TasksAPI.Model;
using TasksAPI.Repository;

namespace TasksAPI.Services;

public class TasksService(ITasksRepository tasksRepository) : ITasksService
{
    public Result<List<TaskDto>> GetTasks(TaskTypeEnum type)
    {
        return tasksRepository.GetTasks(type);
    }

    public Result<TaskDto> GetTask(int id)
    {
        return tasksRepository.GetTask(id);
    }
}
