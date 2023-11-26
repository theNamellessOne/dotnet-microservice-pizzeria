using System.ComponentModel.DataAnnotations;

namespace PizzaService.Models;

public class Pizza
{
    [Key] [Required] public int Id { get; set; }

    [Required] public required string Name { get; set; }
    [Required] public required string Description { get; set; }

    [Required] public double BasePrice { get; set; }

    [Required] public IEnumerable<PizzaSizeOption> SizeOptions { get; set; }

    [Required] public IEnumerable<PizzaBorderOption> BorderOptions { get; set; }
}