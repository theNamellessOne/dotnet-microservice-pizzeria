using PizzaService.Models;

namespace PizzaService.Data.Repositories.Interfaces;

public interface IPizzaRepository
{
    bool SaveChanges();
    void CreatePizza(Pizza pizza);
    void RemovePizza(Pizza pizza);
    Pizza? GetPizzaById(int id);
    IEnumerable<Pizza> GetAllPizzas();
}