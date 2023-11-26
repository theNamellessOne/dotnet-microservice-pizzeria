using PizzaService.Models;

namespace PizzaService.Data.Repositories.Interfaces;

public interface IPizzaBorderOptionsRepository
{
    PizzaBorderOption? GetPizzaBorderOptionById(int id);
    IEnumerable<PizzaBorderOption> GetAllPizzaBorderOptions();
}