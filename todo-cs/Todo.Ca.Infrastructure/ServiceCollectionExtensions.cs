using Microsoft.Extensions.DependencyInjection;
using Todo.Ca.Infrastructure.AggregateRepositories.TodoItem;
using Todo.Domain.Aggregates.TodoItem;

namespace Todo.Ca.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTodoInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITodoItemAggregateRepository, JsonFileTodoItemAggregateRepository>();
        
        return services;
    }
}