using TasksAPI.Dtos;
using TasksAPI.Enum;
using TasksAPI.Model;
using TasksAPI.Repository;

namespace TasksAPI.Services;

public class TasksService(ITasksRepository tasksRepository) : ITasksService
{
    public Result<List<TaskDto>> GetTasks(TaskTypeEnum type)
    {
        if (TaskTypeEnum.Generic == type)
        {
            return Result<List<TaskDto>>.Failure($"Generic type cannot be selected");
        }

        return tasksRepository.GetTasks(type);
    }
}
