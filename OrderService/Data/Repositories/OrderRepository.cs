using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data.Repositories;

public class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void CreateOrder(Order order)
    {
        if (order == null) throw new ArgumentNullException();
        dbContext.Orders.Add(order);
    }

    public Order? GetOrderById(int id)
    {
        return dbContext.Orders.Include(order => order.OrderItems).FirstOrDefault(order => order.Id == id);
    }

    public IEnumerable<Order> GetAllOrders()
    {
        return dbContext.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.Pizza)
            .ToList();
    }
}