using PizzaService.Dtos.PizzaBorderOption;
using PizzaService.Dtos.PizzaSizeOption;

namespace PizzaService.Dtos.Pizza;

public class PizzaReadDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double BasePrice { get; set; }
    public required IEnumerable<PizzaSizeOptionReadDto> SizeOptions { get; set; }
    public required IEnumerable<PizzaBorderOptionReadDto> BorderOptions { get; set; }
}