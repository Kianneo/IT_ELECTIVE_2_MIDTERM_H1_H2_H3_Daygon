using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers;

public class TransactionsController : Controller
{
    // GET: /Transactions
    public IActionResult Index()
    {
        var transactions = StaticDataStore.Transactions.OrderByDescending(t => t.Date).ToList();
        return View(transactions);
    }

    // GET: /Transactions/Details/8A12B34C
    public IActionResult Details(string id)
    {
        var transaction = StaticDataStore.Transactions.FirstOrDefault(t => t.TransactionId == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }
}