namespace FavoriteService.SyncDataServices.Grpc.Pizza;

public interface IPizzaDataClient
{
    IEnumerable<Models.Pizza>? ReturnAllPizzas();
}