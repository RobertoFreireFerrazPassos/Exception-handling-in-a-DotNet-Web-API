using TasksAPI.Repository;
using TasksAPI.Services;

namespace TasksAPI.IoC;

public static class NativeDependencyInjector
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<ITasksRepository, TasksRepository>();
        services.AddSingleton<ITasksService, TasksService>();
    }
}