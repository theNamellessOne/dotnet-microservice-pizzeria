using FavoriteService.Data.Repositories;
using FavoriteService.SyncDataServices.Grpc.Pizza;
using FavoriteService.SyncDataServices.Grpc.User;
using Microsoft.EntityFrameworkCore;

namespace FavoriteService.Data;

public static class PrepareDb
{
    public static void PreparePopulation(IApplicationBuilder applicationBuilder, IHostEnvironment environment)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        //run database migrations and pull database entities from UsersService and PizzaService
        if (environment.IsProduction())
        {
            Migrate(dbContext);
            Pull(serviceScope);
        }
    }

    private static void Migrate(AppDbContext dbContext)
    {
        Console.WriteLine("---> Migrating");
        dbContext.Database.Migrate();
    }

    private static void Pull(IServiceScope serviceScope)
    {
        PullUsers(serviceScope);
        PullPizzas(serviceScope);
    }

    private static void PullPizzas(IServiceScope serviceScope)
    {
        var pizzaRepository = serviceScope.ServiceProvider.GetService<IPizzaRepository>()!;
        var pizzaGrpcClient = serviceScope.ServiceProvider.GetService<IPizzaDataClient>();
        var pizzas = pizzaGrpcClient!.ReturnAllPizzas();

        foreach (var pizza in pizzas!)
        {
            if (pizzaRepository.ExternalPizzaExists(pizza.ExternalId)) continue;

            pizzaRepository.CreatePizza(pizza);
        }

        pizzaRepository.SaveChanges();
    }

    private static void PullUsers(IServiceScope serviceScope)
    {
        var userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>()!;
        var userGrpcClient = serviceScope.ServiceProvider.GetService<IUserDataClient>();
        var users = userGrpcClient!.ReturnAllUsers();
        
        Console.WriteLine("suka");

        foreach (var user in users!)
        {
            if (userRepository.ExternalUserExists(user.ExternalId)) continue;

            userRepository.CreateUser(user);
        }

        userRepository.SaveChanges();
    }
}