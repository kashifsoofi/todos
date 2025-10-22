using Microsoft.Extensions.DependencyInjection;

namespace Todo.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTodoInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITodoRepository, TodoRepository>();
        
        return services;
    }
}