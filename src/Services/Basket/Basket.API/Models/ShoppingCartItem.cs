namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; }
    public string Color { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;

}