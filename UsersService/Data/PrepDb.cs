using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data;

public static class PrepDb
{
    public static void PrepPop(IApplicationBuilder applicationBuilder, IHostEnvironment environment)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        //заповнення бд тестовими даними
        if (environment.IsDevelopment())
            Seed(dbContext);

        //міграція, якщо в 'продакшені', тобто використовуємо реальну бд, а не InMemory
        if (environment.IsProduction())
            Migrate(dbContext);
    }

    private static void Migrate(AppDbContext dbContext)
    {
        Console.WriteLine("--> Мігруємо");
        dbContext.Database.Migrate();
    }

    private static void Seed(AppDbContext dbContext)
    {
        if (dbContext.Users.Any()) return;

        dbContext.Users.AddRange(
            new User
            {
                Email = "email@gmail.com",
                Name = "Валерій",
                Surname = "Газденко",
                Password = "pa55w0rd"
            },
            new User
            {
                Email = "akakiyo@gmail.com",
                Name = "Акакій",
                Surname = "Прокопенко",
                Password = "pa55w0rd"
            },
            new User
            {
                Email = "killReal@gmail.com",
                Name = "Кирило",
                Surname = "Костомахов",
                Password = "pa55w0rd"
            });
        dbContext.SaveChanges();
    }
}