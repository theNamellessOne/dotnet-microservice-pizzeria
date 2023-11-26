namespace PizzaService.Dtos.PizzaSizeOption;

public class PizzaSizeOptionCreateDto
{
    public required string Name { get; set; }
    public required double PriceModifier { get; set; }
}