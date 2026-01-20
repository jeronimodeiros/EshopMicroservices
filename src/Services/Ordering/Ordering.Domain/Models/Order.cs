namespace Ordering.Domain.Models;
public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    private decimal _totalPrice; // backing field persistible

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public decimal TotalPrice { get; private set; }

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public static Order Create(
        OrderId id,
        CustomerId customerId,
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment payment)
    {       
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment payment, 
        OrderStatus status)
    {        
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    // cuando agregues o elimines items actualiza _totalPrice
    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
        var orderItem = new OrderItem(Id, productId, price, quantity);
        _orderItems.Add(orderItem);

        _totalPrice = _orderItems.Sum(i => i.Price * i.Quantity);
    }

    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId.Equals(productId));
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
            _totalPrice = _orderItems.Sum(i => i.Price * i.Quantity);
        }
    }
}
