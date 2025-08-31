namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;


    internal OrderItem(OrderId orderId, ProductId productId, decimal price, int quantity)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

	// Constructor requerido por EF Core
	public OrderItem() { }

}
