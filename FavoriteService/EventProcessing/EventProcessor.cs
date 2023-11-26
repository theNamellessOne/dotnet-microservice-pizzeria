using System.Text.Json;
using AutoMapper;
using FavoriteService.Data.Repositories;
using FavoriteService.Dtos;
using FavoriteService.Models;

namespace FavoriteService.EventProcessing;

public class EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper) : IEventProcessor
{
    //оскільки цей класс буде сінглтон сервісом
    //в нього не можливо заінжектит репозиторії напрям
    //тому це потрібно робити через фабрику скоупів
    public void ProcessEvent(string msg)
    {
        Console.WriteLine("--> Processing Event");
        switch (DetermineEvent(msg))
        {
            case EventType.UserCreated:
                AddUser(msg);
                break;
            case EventType.PizzaCreated:
                AddPizza(msg);
                break;
            case EventType.PizzaRemoved:
                RemovePizza(msg);
                break;
            case EventType.Unknown:
            default: break;
        }
    }

    private void AddUser(string msg)
    {
        Console.WriteLine("--> Спроба додати юзера");
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var userPublishDto = JsonSerializer.Deserialize<UserPublishDto>(msg);

        try
        {
            var userModel = mapper.Map<User>(userPublishDto);

            if (repository.ExternalUserExists(userModel.ExternalId))
                Console.WriteLine($"--> Користувач з айді: {userModel.ExternalId} вже існує");

            repository.CreateUser(userModel);
            repository.SaveChanges();
            Console.WriteLine("--> Успії");
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not add User to DB: {e.Message}");
        }
    }

    private void AddPizza(string msg)
    {
        Console.WriteLine("--> Спроба додати піццу");
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IPizzaRepository>();
        var pizzaPublishDto = JsonSerializer.Deserialize<PizzaPublishDto>(msg);

        try
        {
            var pizzaModel = mapper.Map<Pizza>(pizzaPublishDto);

            if (repository.ExternalPizzaExists(pizzaModel.ExternalId))
                Console.WriteLine($"--> Pizza with {pizzaModel.ExternalId} already exists");

            repository.CreatePizza(pizzaModel);
            repository.SaveChanges();
            Console.WriteLine("--> Success");
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not add Pizza to DB: {e.Message}");
        }
    }

    private void RemovePizza(string msg)
    {
        Console.WriteLine("--> Trying to remove Pizza");
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IPizzaRepository>();
        var pizzaPublishDto = JsonSerializer.Deserialize<PizzaPublishDto>(msg);

        try
        {
            var pizzaModel = mapper.Map<Pizza>(pizzaPublishDto);

            if (!repository.ExternalPizzaExists(pizzaModel.ExternalId))
                Console.WriteLine($"--> Pizza with {pizzaModel.ExternalId} does not exist");

            repository.RemovePizza(pizzaModel);
            repository.SaveChanges();
            Console.WriteLine("--> Success");
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not remove Pizza from DB: {e.Message}");
        }
    }

    private static EventType DetermineEvent(string msg)
    {
        Console.WriteLine($"--> Отрацювання повідомлення: {msg}");
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(msg);
        Console.WriteLine($"--> Тип повідомлення: {eventType?.Event}");

        return eventType?.Event switch
        {
            "User_Created" => EventType.UserCreated,
            "Pizza_Created" => EventType.PizzaCreated,
            "Pizza_Removed" => EventType.PizzaRemoved,
            _ => EventType.Unknown
        };
    }
}

internal enum EventType
{
    UserCreated,
    PizzaCreated,
    PizzaRemoved,
    Unknown
}