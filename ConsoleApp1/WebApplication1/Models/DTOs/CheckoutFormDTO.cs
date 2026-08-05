using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

public class CheckoutFormDTO
{
    [Required(ErrorMessage = "Customer Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string CustomerName { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string? CustomerEmail { get; set; }
}