using AutoMapper;
using Grpc.Core;
using PizzaService.Data.Repositories.Interfaces;

namespace PizzaService.SyncDataServices.Grpc;

//GrpcPizzaBase генериться автоматично на базі coffee.proto
public class GrpcPizzaService(IPizzaRepository pizzaRepository, IMapper mapper) : GrpcPizza.GrpcPizzaBase
{
    //імплементація протобаф метода для отримання всіх піц, виконується при грпс запиті з іншого сервіса 
    public override Task<PizzaResponse> GetAllPizzas(GetAllRequest request, ServerCallContext context)
    {
        var pizzas = pizzaRepository.GetAllPizzas();
        var response = new PizzaResponse();

        foreach (var pizza in pizzas) response.Pizza.Add(mapper.Map<GrpcPizzaModel>(pizza));

        return Task.FromResult(response);
    }
}