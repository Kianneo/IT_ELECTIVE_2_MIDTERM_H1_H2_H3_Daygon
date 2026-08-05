using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Controllers;

public class CheckoutController : Controller
{
    public IActionResult Index()
    {
        var cart = StaticDataStore.ActiveCart;
        if (cart == null || !cart.Items.Any())
        {
            return RedirectToAction("Index", "Cart");
        }

        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProcessPayment(CheckoutFormDTO dto)
    {
        var cart = StaticDataStore.ActiveCart;

        if (cart == null || !cart.Items.Any())
        {
            return RedirectToAction("Index", "Cart");
        }

        if (!ModelState.IsValid)
        {
            return View("Index", cart);
        }

        if (dto.AmountTendered < cart.GrandTotal)
        {
            ModelState.AddModelError("AmountTendered", $"Amount tendered (${dto.AmountTendered:F2}) is less than total due (${cart.GrandTotal:F2}).");
            return View("Index", cart);
        }

        // Deduct inventory stock permanently (US-05 AC4)
        foreach (var cartItem in cart.Items)
        {
            var product = StaticDataStore.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
            if (product != null)
            {
                product.StockQuantity -= cartItem.Quantity;
            }
        }

        // Create transaction record (US-05 AC3)
        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid().ToString("N")[..8].ToUpper(),
            Date = DateTime.Now,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            TotalAmount = cart.GrandTotal,
            AmountTendered = dto.AmountTendered,
            ChangeDue = dto.AmountTendered - cart.GrandTotal,
            PurchasedItems = new List<CartItem>(cart.Items)
        };

        StaticDataStore.Transactions.Add(transaction);

        // Clear active cart (US-05 AC5)
        cart.Items.Clear();

        return View("Receipt", transaction);
    }
}