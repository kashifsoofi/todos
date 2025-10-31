namespace Todo.Domain.Interfaces;

public interface IAggregateEvent
{
    DateTimeOffset OccurredAt { get; }
}
