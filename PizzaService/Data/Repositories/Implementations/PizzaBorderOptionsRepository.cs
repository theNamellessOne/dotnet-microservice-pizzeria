using PizzaService.Data.Repositories.Interfaces;
using PizzaService.Models;

namespace PizzaService.Data.Repositories.Implementations;

public class PizzaBorderOptionsRepository(AppDbContext dbContext) : IPizzaBorderOptionsRepository
{
    public PizzaBorderOption? GetPizzaBorderOptionById(int id)
    {
        return dbContext.PizzaBorderOption.FirstOrDefault(pizzaBorderOption => pizzaBorderOption.Id == id);
    }

    public IEnumerable<PizzaBorderOption> GetAllPizzaBorderOptions()
    {
        return dbContext.PizzaBorderOption.ToList();
    }
}