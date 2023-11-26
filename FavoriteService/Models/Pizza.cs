using System.ComponentModel.DataAnnotations;

namespace FavoriteService.Models;

public class Pizza
{
    [Key] [Required] public int Id { get; set; }

    [Required] public int ExternalId { get; set; }

    [Required] public required string Name { get; set; }

    [Required] public required string Description { get; set; }

    [Required] public required double BasePrice { get; set; }

    public ICollection<User>? FavoredBy { get; set; }
}