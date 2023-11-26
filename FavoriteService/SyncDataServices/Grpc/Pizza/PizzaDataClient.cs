using AutoMapper;
using Grpc.Net.Client;
using PizzaService;

namespace FavoriteService.SyncDataServices.Grpc.Pizza;

public class PizzaDataClient(IConfiguration configuration, IMapper mapper) : IPizzaDataClient
{
    public IEnumerable<Models.Pizza>? ReturnAllPizzas()
    {
        Console.WriteLine($"--> Calling GRPC PizzaService {configuration["GrpcPizza"]}");
        var channel = GrpcChannel.ForAddress(configuration["GrpcPizza"]!);
        var client = new GrpcPizza.GrpcPizzaClient(channel);
        var request = new GetAllRequest();

        try
        {
            var response = client.GetAllPizzas(request);
            return mapper.Map<IEnumerable<Models.Pizza>>(response.Pizza);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not call GRPC PizzaService {configuration["GrpcPizza"]}");
            Console.WriteLine($"--> Помилка: {e.Message}");
            return null;
        }
    }
}