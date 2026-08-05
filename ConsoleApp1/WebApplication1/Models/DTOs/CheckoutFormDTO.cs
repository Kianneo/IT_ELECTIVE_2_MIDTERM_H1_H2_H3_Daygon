using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

public class CheckoutFormDTO
{
    [Required(ErrorMessage = "Customer Name is required.")]
    [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
    public string CustomerName { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? CustomerEmail { get; set; }

    [Required(ErrorMessage = "Amount tendered is required.")]
    [Range(0.01, 10000.00, ErrorMessage = "Amount tendered must be greater than $0.00.")]
    public decimal AmountTendered { get; set; }
}