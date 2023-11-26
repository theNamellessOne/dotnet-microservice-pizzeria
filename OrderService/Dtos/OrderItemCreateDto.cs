namespace OrderService.Dtos;

public class OrderItemCreateDto
{
    public required int PizzaId { get; set; }
    public required int SizeOptionId { get; set; }
    public required int BorderOptionId { get; set; }
    public required int Amount { get; set; }
}