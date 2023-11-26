using OrderService.Models;

namespace OrderService.Dtos;

public class OrderItemReadDto
{
    public required int Id { get; set; }
    public required int Amount { get; set; }
    public required double Price { get; set; }
    public required string SizeOption { get; set; }
    public required string BorderOption { get; set; }
    public required int PizzaId { get; set; }
    public required int SizeOptionId { get; set; }
    public required int BorderOptionId { get; set; }
    public required Pizza Pizza { get; set; }
    public required int OrderId { get; set; }
}