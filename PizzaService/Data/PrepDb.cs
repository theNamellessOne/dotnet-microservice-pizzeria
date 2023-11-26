using Microsoft.EntityFrameworkCore;

namespace PizzaService.Data;

public static class PrepDb
{
    public static void PreparePopulation(IApplicationBuilder applicationBuilder, IHostEnvironment environment)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        //міграція
        if (environment.IsProduction()) Migrate(dbContext);
    }

    private static void Migrate(AppDbContext dbContext)
    {
        Console.WriteLine("--> Мігруємо");
        dbContext.Database.Migrate();
    }
}