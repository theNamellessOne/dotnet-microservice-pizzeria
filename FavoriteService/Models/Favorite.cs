namespace FavoriteService.Models;

public class Favorite
{
    public int UserId { get; set; }
    public int PizzaId { get; set; }
    public Pizza? Pizza { get; set; }
}