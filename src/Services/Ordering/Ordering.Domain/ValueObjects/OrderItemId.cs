namespace Ordering.Domain.ValueObjects;

public record struct OrderItemId(Guid Value); // Changed to 'record struct' to make it a non-nullable value type.
