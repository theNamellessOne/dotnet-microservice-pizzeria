using System.ComponentModel.DataAnnotations;

namespace OrderService.Models;

public class OrderItem
{
    [Key] [Required] public int Id { get; set; }

    [Required] public int Amount { get; set; }

    [Required] public double Price { get; set; }

    public int PizzaId { get; set; }
    public required string SizeOption { get; set; }
    public required string BorderOption { get; set; }
    public required int SizeOptionId { get; set; }
    public required int BorderOptionId { get; set; }
    public Pizza? Pizza { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
}