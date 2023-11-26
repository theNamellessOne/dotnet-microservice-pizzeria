using System.Text.Json;
using AutoMapper;
using OrderService.Data.Repositories;
using OrderService.Dtos;
using OrderService.Models;

namespace OrderService.EventProcessing;

public class EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper) : IEventProcessor
{
    //since event processor is a singleton service
    //it is impossible to inject scoped repositories directly
    //so it is necessary to create them using scope factory
    public void ProcessEvent(string msg)
    {
        Console.WriteLine("---> Processing Event");
        switch (DetermineEvent(msg))
        {
            case EventType.PizzaCreated:
                AddPizza(msg);
                break;
            case EventType.Unknown:
            default: break;
        }
    }

    private void AddPizza(string msg)
    {
        Console.WriteLine("---> Trying to add Pizza");
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IPizzaRepository>();
        var pizzaPublishDto = JsonSerializer.Deserialize<PizzaPublishDto>(msg);

        try
        {
            var pizzaModel = mapper.Map<Pizza>(pizzaPublishDto);

            if (repository.ExternalPizzaExists(pizzaModel.ExternalId))
                Console.WriteLine($"---> Pizza with {pizzaModel.ExternalId} already exists");

            repository.CreatePizza(pizzaModel);
            repository.SaveChanges();
            Console.WriteLine("---> Success");
        }
        catch (Exception e)
        {
            Console.WriteLine($"---> Could not add Pizza to DB: {e.Message}");
        }
    }

    private static EventType DetermineEvent(string msg)
    {
        Console.WriteLine($"---> Determining Event: {msg}");
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(msg);
        Console.WriteLine($"---> Determined Event: {eventType?.Event}");

        return eventType?.Event switch
        {
            "Pizza_Created" => EventType.PizzaCreated,
            _ => EventType.Unknown
        };
    }
}

internal enum EventType
{
    PizzaCreated,
    Unknown
}