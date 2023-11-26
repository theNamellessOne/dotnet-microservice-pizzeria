using OrderService.Models;

namespace OrderService.SyncDataServices.Grpc;

public interface IGrpcPizzaDataClient
{
    IEnumerable<Pizza>? ReturnAllPizzas();
}