using WebApplication1.Models.Domain;

namespace WebApplication1.Data;

public static class StaticDataStore
{
    public static ShoppingCart ActiveCart { get; set; } = new();

    public static List<Transaction> Transactions { get; set; } = new();

    public static List<Product> Products { get; set; } = new()
    {
        new Product { Id = 1, Name = "Artisanal Sourdough Bread", Price = 6.50m, StockQuantity = 15 },
        new Product { Id = 2, Name = "Butter Croissant", Price = 3.25m, StockQuantity = 20 },
        new Product { Id = 3, Name = "Chocolate Danish", Price = 4.00m, StockQuantity = 12 },
        new Product { Id = 4, Name = "Blueberry Muffin", Price = 3.50m, StockQuantity = 18 },
        new Product { Id = 5, Name = "Cinnamon Roll", Price = 4.50m, StockQuantity = 8 },
        new Product { Id = 6, Name = "Baguette", Price = 4.00m, StockQuantity = 10 },
        new Product { Id = 7, Name = "Almond Croissant", Price = 4.75m, StockQuantity = 5 },
        new Product { Id = 8, Name = "Whole Wheat Bread", Price = 5.50m, StockQuantity = 0 } // Out of stock example
    };
}