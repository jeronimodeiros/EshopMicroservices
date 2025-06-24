namespace Ordering.Domain.ValueObjects;

public readonly record struct CustomerId(Guid Value) : IEquatable<CustomerId>
{
    public override string ToString() => Value.ToString();
}
