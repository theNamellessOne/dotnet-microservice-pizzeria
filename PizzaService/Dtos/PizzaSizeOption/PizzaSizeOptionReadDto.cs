namespace PizzaService.Dtos.PizzaSizeOption;

public class PizzaSizeOptionReadDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required double PriceModifier { get; set; }
    public required int PizzaId { get; set; }
}