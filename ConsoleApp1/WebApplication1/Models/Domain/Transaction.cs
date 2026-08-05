namespace WebApplication1.Models.Domain;

public class Transaction
{
    public string TransactionId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? CustomerEmail { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AmountTendered { get; set; }
    public decimal ChangeDue { get; set; }
    public List<CartItem> PurchasedItems { get; set; } = new();
}