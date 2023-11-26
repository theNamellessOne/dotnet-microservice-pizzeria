using Microsoft.EntityFrameworkCore;
using OrderService.Data.Repositories;
using OrderService.SyncDataServices.Grpc;

namespace OrderService.Data;

public static class PrepareDb
{
    public static void PreparePopulation(IApplicationBuilder applicationBuilder, IHostEnvironment environment)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        //run database migrations and pull database entities from PizzaService
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
        var pizzaRepository = serviceScope.ServiceProvider.GetService<IPizzaRepository>();
        var pizzaDataClient = serviceScope.ServiceProvider.GetService<IGrpcPizzaDataClient>();
        var pizzas = pizzaDataClient!.ReturnAllPizzas();

        foreach (var pizza in pizzas!)
        {
            Console.Write(pizza.Id);
            Console.Write(";");
            Console.Write(pizza.ExternalId);
            Console.Write(";");
            Console.Write(pizza.Name);
            Console.WriteLine(";");
            if (pizzaRepository!.ExternalPizzaExists(pizza.ExternalId)) continue;

            pizzaRepository.CreatePizza(pizza);
        }

        pizzaRepository!.SaveChanges();
    }
}