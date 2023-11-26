using OrderService.Models;

namespace OrderService.Data.Repositories;

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

    public bool ExternalPizzaExists(int externalPizzaId)
    {
        return dbContext.Pizzas.Any(pizza => pizza.ExternalId == externalPizzaId);
    }

    public bool PizzaExistsByExternalId(int externalId)
    {
        return dbContext.Pizzas.Any(pizza => pizza.ExternalId == externalId);
    }

    public IEnumerable<Pizza> GetAllPizzas()
    {
        return dbContext.Pizzas.ToList();
    }

    public Pizza GetPizzaByExternalId(int externalId)
    {
        return dbContext.Pizzas.FirstOrDefault(pizza => pizza.ExternalId == externalId)!;
    }
}