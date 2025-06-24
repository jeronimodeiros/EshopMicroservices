namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public Guid OrderId { get; private set; } = default!;
    public Guid ProductId { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;


    public OrderItem(OrderId orderId, ProductId productId, decimal price, int quantity)
    {
        //Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId.Value;
        ProductId = productId.Value;
        Price = price;
        Quantity = quantity;
    }

}
