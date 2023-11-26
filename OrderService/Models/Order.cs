using System.ComponentModel.DataAnnotations;

namespace OrderService.Models;

public class Order
{
    [Key] [Required] public int Id { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string Address { get; set; }

    [Required] public double Total { get; set; }

    [Required] public ICollection<OrderItem> OrderItems { get; set; }
}