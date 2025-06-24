namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    public required string Value { get; init; }
}
