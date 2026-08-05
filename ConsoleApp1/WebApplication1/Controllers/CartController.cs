using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    // GET: /Cart
    public IActionResult Index()
    {
        var cart = StaticDataStore.ActiveCart;
        return View(cart);
    }

    // POST: /Cart/AddToCart
    [HttpPost]
    public IActionResult AddToCart(AddToCartDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        var product = StaticDataStore.Products.FirstOrDefault(p => p.Id == dto.ProductId);
        if (product == null || product.StockQuantity < dto.Quantity)
        {
            TempData["Error"] = "Selected quantity exceeds available stock.";
            return RedirectToAction(nameof(Index));
        }

        var cart = StaticDataStore.ActiveCart;
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;
        }
        else
        {
            cart.Items.Add(new Models.Domain.CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = dto.Quantity
            });
        }

        return RedirectToAction(nameof(Index));
    }

    // POST: /Cart/UpdateQuantity
    [HttpPost]
    public IActionResult UpdateQuantity(UpdateCartDTO dto)
    {
        var cart = StaticDataStore.ActiveCart;
        var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

        if (item != null)
        {
            if (dto.Quantity <= 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                var product = StaticDataStore.Products.FirstOrDefault(p => p.Id == dto.ProductId);
                if (product != null && dto.Quantity <= product.StockQuantity)
                {
                    item.Quantity = dto.Quantity;
                }
            }
        }

        return RedirectToAction(nameof(Index));
    }

    // POST: /Cart/RemoveFromCart
    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        var cart = StaticDataStore.ActiveCart;
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
        }

        return RedirectToAction(nameof(Index));
    }
}