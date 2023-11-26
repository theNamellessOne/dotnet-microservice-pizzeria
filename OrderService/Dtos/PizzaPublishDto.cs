namespace OrderService.Dtos;

public class PizzaPublishDto : GenericEventDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double BasePrice { get; set; }
}