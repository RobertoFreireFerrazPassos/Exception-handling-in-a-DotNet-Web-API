using TasksAPI.Enum;

namespace TasksAPI.Dtos;

public class TaskDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public TaskTypeEnum Type { get; set; }
}