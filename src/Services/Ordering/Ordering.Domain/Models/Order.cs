namespace Ordering.Domain.Models;
public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public Guid CustomerId { get; private set; } = default!;
    public string OrderName { get; private set; } = string.Empty;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice => _orderItems.Sum(item => item.Price * item.Quantity);
}
