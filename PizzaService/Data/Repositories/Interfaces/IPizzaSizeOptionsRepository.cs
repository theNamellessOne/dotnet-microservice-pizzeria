using PizzaService.Models;

namespace PizzaService.Data.Repositories.Interfaces;

public interface IPizzaSizeOptionsRepository
{
    PizzaSizeOption? GetPizzaSizeOptionById(int id);
    IEnumerable<PizzaSizeOption> GetAllPizzaSizeOptions();
}