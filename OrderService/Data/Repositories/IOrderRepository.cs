using OrderService.Models;

namespace OrderService.Data.Repositories;

public interface IOrderRepository
{
    bool SaveChanges();
    void CreateOrder(Order order);
    Order? GetOrderById(int id);
    IEnumerable<Order> GetAllOrders();
}