namespace PizzaService.Dtos.PizzaBorderOption;

public class PizzaBorderOptionCreateDto
{
    public required string Name { get; set; }
    public required double PriceModifier { get; set; }
}