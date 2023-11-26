using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    //representation of database table `Pizzas`
    public required DbSet<Pizza> Pizzas { get; set; }

    //representation of database table `Orders`
    public required DbSet<Order> Orders { get; set; }

    //representation of database table `Orders`
    public required DbSet<OrderItem> OrderItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //creation of manyToOne relation
        //Order has many OrderItems 
        //OrderItem has one Order 
        //OrderItem has foreign key OrderId
        modelBuilder.Entity<Order>()
            .HasMany(e => e.OrderItems)
            .WithOne(e => e.Order)
            .HasForeignKey(e => e.OrderId)
            .IsRequired();
    }
}