namespace PizzaService.Dtos.Pizza;

public class PizzaPublishDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Event { get; set; }
    public required string Description { get; set; }
    public required double BasePrice { get; set; }
}