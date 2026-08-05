namespace WebApplication1.Models.Domain;

public class ShoppingCart
{
    public List<CartItem> Items { get; set; } = new();
    public decimal GrandTotal => Items.Sum(i => i.LineTotal);
}