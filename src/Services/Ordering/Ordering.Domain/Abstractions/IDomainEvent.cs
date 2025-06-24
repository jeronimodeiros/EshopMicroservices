using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();

    DateTime OccurredOn => DateTime.Now;

    string EventType => GetType().AssemblyQualifiedName ?? string.Empty;
}
