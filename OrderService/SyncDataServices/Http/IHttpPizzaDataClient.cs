using OrderService.Dtos.HttpGetDtos;

namespace OrderService.SyncDataServices.Http;

public interface IHttpPizzaDataClient
{
    Task<PizzaHttpGetDto?> GetPizzaById(int id);
}