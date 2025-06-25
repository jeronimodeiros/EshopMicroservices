namespace Ordering.Domain.ValueObjects;

public readonly record struct OrderId : IEquatable<OrderId>
{
    public Guid Value { get; init; }
    public override string ToString() => Value.ToString();
}
