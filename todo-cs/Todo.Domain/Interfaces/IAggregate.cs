namespace Todo.Domain.Interfaces;

public interface IAggregate
{
    IReadOnlyCollection<IAggregateEvent> UncommittedEvents { get; }
}
