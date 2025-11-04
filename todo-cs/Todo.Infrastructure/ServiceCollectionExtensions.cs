using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Aggregates.TodoItem;
using Todo.Infrastructure.AggregateRepositories.TodoItem;

namespace Todo.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTodoInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITodoItemAggregateRepository, JsonFileTodoItemAggregateRepository>();
        
        return services;
    }
}