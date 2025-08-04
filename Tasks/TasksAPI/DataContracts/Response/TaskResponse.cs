using TasksAPI.Dtos;

namespace TasksAPI.DataContracts.Response;

public class TaskResponse
{
    public List<TaskDto> Tasks { get; set; }
}
