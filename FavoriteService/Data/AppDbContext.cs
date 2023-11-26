using FavoriteService.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoriteService.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    //таблиця користувачів
    public required DbSet<User> Users { get; set; }

    //таблиця піц
    public required DbSet<Pizza> Pizzas { get; set; }

    //такблиця улюбленних піц
    public required DbSet<Favorite> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //creation of manyToMany relation
        //User has many FavoritePizzas
        //Pizza is Favored By many User
        //Favorite is a table representation of manyToMany with ids
        modelBuilder.Entity<User>()
            .HasMany(e => e.FavoritePizzas)
            .WithMany(e => e.FavoredBy)
            .UsingEntity<Favorite>();
    }
}