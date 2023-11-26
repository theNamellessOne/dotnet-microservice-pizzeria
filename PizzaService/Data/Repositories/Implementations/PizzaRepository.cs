using Microsoft.EntityFrameworkCore;
using PizzaService.Data.Repositories.Interfaces;
using PizzaService.Models;

namespace PizzaService.Data.Repositories.Implementations;

public class PizzaRepository(AppDbContext dbContext) : IPizzaRepository
{
    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void CreatePizza(Pizza pizza)
    {
        if (pizza == null) throw new ArgumentNullException();
        dbContext.Pizza.Add(pizza);
    }

    public void RemovePizza(Pizza pizza)
    {
        if (pizza == null) throw new ArgumentNullException();
        dbContext.Pizza.Remove(pizza);
    }

    public Pizza? GetPizzaById(int id)
    {
        return dbContext.Pizza
            .Include(pizza => pizza.SizeOptions)
            .Include(pizza => pizza.BorderOptions)
            .FirstOrDefault(pizza => pizza.Id == id);
    }

    public IEnumerable<Pizza> GetAllPizzas()
    {
        return dbContext.Pizza
            .Include(pizza => pizza.SizeOptions)
            .Include(pizza => pizza.BorderOptions)
            .ToList();
    }
}