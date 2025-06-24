
namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
  where TId : struct // Ensure TId is a non-nullable value type
{
    private readonly List<IDomainEvent> _domainEvents = new();

    // Removed 'override' as the base class Entity<TId> does not define this property
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    // Removed 'override' as the base class Entity<TId> does not define this method
    public IDomainEvent[] ClearDomainEvents()
    {
        var dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeuedEvents;
    }
}
