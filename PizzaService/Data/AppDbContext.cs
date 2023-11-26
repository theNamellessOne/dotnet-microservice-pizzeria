using Microsoft.EntityFrameworkCore;
using PizzaService.Models;

namespace PizzaService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    //таблиця бази данних 
    public required DbSet<PizzaBorderOption> PizzaBorderOption { get; set; }

    //таблиця бази данних 
    public required DbSet<Pizza> Pizza { get; set; }

    //таблиця бази данних 
    public required DbSet<PizzaSizeOption> PizzaSizeOption { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //створення зв'язків один до багатьох

        //піцца має багато розмірів
        //розмір має одну піцу 
        modelBuilder.Entity<Pizza>()
            .HasMany(e => e.SizeOptions)
            .WithOne(e => e.Pizza)
            .HasForeignKey(e => e.PizzaId)
            .IsRequired();

        //піцца має багато бортиків 
        //бортик має одну піцу 
        modelBuilder.Entity<Pizza>()
            .HasMany(e => e.BorderOptions)
            .WithOne(e => e.Pizza)
            .HasForeignKey(e => e.PizzaId)
            .IsRequired();
    }
}