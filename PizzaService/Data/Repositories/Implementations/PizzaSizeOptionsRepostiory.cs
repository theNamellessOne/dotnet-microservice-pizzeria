using PizzaService.Data.Repositories.Interfaces;
using PizzaService.Models;

namespace PizzaService.Data.Repositories.Implementations;

public class PizzaSizeOptionsRepostiory(AppDbContext dbContext) : IPizzaSizeOptionsRepository
{
    public PizzaSizeOption? GetPizzaSizeOptionById(int id)
    {
        return dbContext.PizzaSizeOption.FirstOrDefault(pizzaBorderOption => pizzaBorderOption.Id == id);
    }

    public IEnumerable<PizzaSizeOption> GetAllPizzaSizeOptions()
    {
        return dbContext.PizzaSizeOption.ToList();
    }
}