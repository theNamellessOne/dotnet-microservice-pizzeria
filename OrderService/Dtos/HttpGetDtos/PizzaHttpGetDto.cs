namespace OrderService.Dtos.HttpGetDtos;

public class PizzaHttpGetDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double BasePrice { get; set; }
    public required IEnumerable<PizzaSizeOptionHttpGetDto> SizeOptions { get; set; }
    public required IEnumerable<PizzaBorderOptionHttpGetDto> BorderOptions { get; set; }
}