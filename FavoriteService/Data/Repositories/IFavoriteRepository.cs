using FavoriteService.Models;

namespace FavoriteService.Data.Repositories;

public interface IFavoriteRepository
{
    bool SaveChanges();
    Favorite? GetFavoriteByUserIdAndPizzaId(int userId, int pizzaId);
    IEnumerable<Pizza> GetUserFavoritePizzas(int userId);
    void AddToUserFavorites(int userId, Pizza pizza);
    void RemoveFromUserFavorites(Favorite favorite);
}