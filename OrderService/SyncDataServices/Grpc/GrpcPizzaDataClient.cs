using AutoMapper;
using Grpc.Net.Client;
using OrderService.Models;
using PizzaService;

namespace OrderService.SyncDataServices.Grpc;

public class GrpcPizzaDataClient(IConfiguration configuration, IMapper mapper) : IGrpcPizzaDataClient
{
    public IEnumerable<Pizza>? ReturnAllPizzas()
    {
        Console.WriteLine($"--> Визиваємо Грпс сервіс піц {configuration["GrpcPizza"]}");
        var channel = GrpcChannel.ForAddress(configuration["GrpcPizza"]!);
        var client = new GrpcPizza.GrpcPizzaClient(channel);
        var request = new GetAllRequest();

        try
        {
            var response = client.GetAllPizzas(request);
            return mapper.Map<IEnumerable<Pizza>>(response.Pizza);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Error {e.Message}");
            return null;
        }
    }
}