using System.ComponentModel.DataAnnotations;

namespace PizzaService.Models;

public class PizzaSizeOption
{
    [Key] [Required] public int Id { get; set; }

    [Required] public required string Name { get; set; }

    [Required] public required double PriceModifier { get; set; }

    [Required] public int PizzaId { get; set; }

    [Required] public Pizza? Pizza { get; set; }
}