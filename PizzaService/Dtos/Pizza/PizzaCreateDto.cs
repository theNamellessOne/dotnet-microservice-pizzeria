using PizzaService.Dtos.PizzaBorderOption;
using PizzaService.Dtos.PizzaSizeOption;

namespace PizzaService.Dtos.Pizza;

public class PizzaCreateDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double BasePrice { get; set; }
    public required IEnumerable<PizzaSizeOptionCreateDto> SizeOptions { get; set; }
    public required IEnumerable<PizzaBorderOptionCreateDto> BorderOptions { get; set; }
}