using FavoriteService.Models;

namespace FavoriteService.Data.Repositories;

public class PizzaRepository(AppDbContext dbContext) : IPizzaRepository
{
    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void CreatePizza(Pizza pizza)
    {
        if (pizza == null) throw new ArgumentNullException();
        dbContext.Pizzas.Add(pizza);
    }

    public void RemovePizza(Pizza pizza)
    {
        dbContext.Pizzas.Remove(pizza);
    }

    public bool ExternalPizzaExists(int externalPizzaId)
    {
        return dbContext.Pizzas.Any(pizza => pizza.ExternalId == externalPizzaId);
    }

    public Pizza? GetPizzaById(int externalId)
    {
        return dbContext.Pizzas.FirstOrDefault(pizza => pizza.ExternalId == externalId);
    }

    public IEnumerable<Pizza> GetAllPizzas()
    {
        return dbContext.Pizzas.ToList();
    }
}