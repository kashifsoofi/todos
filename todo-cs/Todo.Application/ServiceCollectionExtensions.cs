using Microsoft.Extensions.DependencyInjection;

namespace Todo.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTodoServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}