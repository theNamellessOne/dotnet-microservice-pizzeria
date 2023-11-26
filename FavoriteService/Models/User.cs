using System.ComponentModel.DataAnnotations;

namespace FavoriteService.Models;

public class User
{
    [Key] [Required] public int Id { get; set; }

    [Required] public int ExternalId { get; set; }

    [Required] public string? Name { get; set; }
    [Required] public string? Surname { get; set; }

    public ICollection<Pizza>? FavoritePizzas { get; set; }
}