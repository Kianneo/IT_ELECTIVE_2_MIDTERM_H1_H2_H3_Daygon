namespace WebApplication1.Models.Domain;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public decimal SubTotal => UnitPrice * Quantity;
}