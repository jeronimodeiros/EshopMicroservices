namespace Ordering.Domain.ValueObjects;

public readonly record struct ProductId(Guid Value) : IEquatable<ProductId>;
