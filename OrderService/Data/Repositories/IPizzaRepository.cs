using OrderService.Models;

namespace OrderService.Data.Repositories;

public interface IPizzaRepository
{
    bool SaveChanges();
    void CreatePizza(Pizza pizza);
    Pizza GetPizzaByExternalId(int externalId);
    bool ExternalPizzaExists(int externalPizzaId);
    bool PizzaExistsByExternalId(int externalId);
    IEnumerable<Pizza> GetAllPizzas();
}