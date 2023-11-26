using PizzaService.Dtos.Pizza;

namespace PizzaService.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishPizza(PizzaPublishDto pizzaPublishDto);
}