namespace Ordering.Domain.Abstractions;

public interface IAggregate<T> : IAggregate, IEntity<T>
{
    new T Id { get; set; }
}

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    IDomainEvent[] ClearDomainEvents();
}
