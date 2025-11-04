namespace Todo.Domain.Interfaces;

public record AggregateEvent : IAggregateEvent
{
    public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow;
}
