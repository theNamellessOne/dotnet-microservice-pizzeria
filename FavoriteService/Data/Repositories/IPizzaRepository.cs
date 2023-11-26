using FavoriteService.Models;

namespace FavoriteService.Data.Repositories;

public interface IPizzaRepository
{
    bool SaveChanges();
    void CreatePizza(Pizza pizza);
    void RemovePizza(Pizza pizza);
    bool ExternalPizzaExists(int externalPizzaId);
    Pizza? GetPizzaById(int externalId);
    IEnumerable<Pizza> GetAllPizzas();
}