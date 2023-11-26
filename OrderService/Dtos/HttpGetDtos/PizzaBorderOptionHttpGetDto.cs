namespace OrderService.Dtos.HttpGetDtos;

public class PizzaBorderOptionHttpGetDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required double PriceModifier { get; set; }
    public required int PizzaId { get; set; }
}