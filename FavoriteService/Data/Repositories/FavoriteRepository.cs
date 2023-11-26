using FavoriteService.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoriteService.Data.Repositories;

public class FavoriteRepository(AppDbContext dbContext) : IFavoriteRepository
{
    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public Favorite? GetFavoriteByUserIdAndPizzaId(int userId, int pizzaId)
    {
        return dbContext.Favorites.FirstOrDefault(favorite =>
            favorite.UserId == userId && favorite.PizzaId == pizzaId);
    }

    public IEnumerable<Pizza> GetUserFavoritePizzas(int userId)
    {
        List<Pizza> userFavoritePizzas = new();
        var userFavorites = dbContext.Favorites.Where(favorite => favorite.UserId == userId)
            .Include(favorite => favorite.Pizza).ToList();

        foreach (var favorite in userFavorites)
            if (favorite.Pizza != null)
                userFavoritePizzas.Add(favorite.Pizza);

        return userFavoritePizzas;
    }

    public void AddToUserFavorites(int userId, Pizza pizza)
    {
        dbContext.Favorites.Add(new Favorite
        {
            UserId = userId,
            PizzaId = pizza.Id
        });
    }

    public void RemoveFromUserFavorites(Favorite favorite)
    {
        dbContext.Favorites.Remove(favorite);
    }
}