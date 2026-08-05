using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Controllers;

public class CartController : Controller
{
    // US-01: View Catalog & Active Cart
    public IActionResult Index()
    {
        ViewBag.Products = StaticDataStore.Products;
        return View(StaticDataStore.ActiveCart);
    }

    // US-02: Add Product to Cart
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddToCart(AddToCartDTO dto)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Invalid request details.";
            return RedirectToAction(nameof(Index));
        }

        var product = StaticDataStore.Products.FirstOrDefault(p => p.Id == dto.ProductId);
        if (product == null)
        {
            TempData["Error"] = "Product not found.";
            return RedirectToAction(nameof(Index));
        }

        var existingItem = StaticDataStore.ActiveCart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        int currentCartQty = existingItem?.Quantity ?? 0;

        if (currentCartQty + dto.Quantity > product.StockQuantity)
        {
            TempData["Error"] = $"Cannot add item. Requested quantity exceeds available stock ({product.StockQuantity}).";
            return RedirectToAction(nameof(Index));
        }

        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;
        }
        else
        {
            StaticDataStore.ActiveCart.Items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = dto.Quantity
            });
        }

        TempData["Success"] = $"{product.Name} added to cart.";
        return RedirectToAction(nameof(Index));
    }

    // US-03: Update Quantity in Cart
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateQuantity(UpdateCartDTO dto)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Invalid quantity specified.";
            return RedirectToAction(nameof(Index));
        }

        var existingItem = StaticDataStore.ActiveCart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        var product = StaticDataStore.Products.FirstOrDefault(p => p.Id == dto.ProductId);

        if (existingItem == null || product == null)
        {
            TempData["Error"] = "Item not found in cart.";
            return RedirectToAction(nameof(Index));
        }

        if (dto.Quantity > product.StockQuantity)
        {
            TempData["Error"] = $"Cannot update quantity. Only {product.StockQuantity} available in stock.";
            return RedirectToAction(nameof(Index));
        }

        existingItem.Quantity = dto.Quantity;
        TempData["Success"] = "Cart updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    // US-04: Remove Item from Cart
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveItem(int productId)
    {
        var itemToRemove = StaticDataStore.ActiveCart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (itemToRemove != null)
        {
            StaticDataStore.ActiveCart.Items.Remove(itemToRemove);
            TempData["Success"] = $"{itemToRemove.ProductName} removed from cart.";
        }

        return RedirectToAction(nameof(Index));
    }
}