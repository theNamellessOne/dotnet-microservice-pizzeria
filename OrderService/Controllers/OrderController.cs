using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Repositories;
using OrderService.Dtos;
using OrderService.Models;
using OrderService.SyncDataServices.Http;

namespace OrderService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(
    IHttpPizzaDataClient httpPizzaDataClient,
    IPizzaRepository pizzaRepository,
    IOrderRepository orderRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public ActionResult<OrderReadDto> GetAllOrders()
    {
        return Ok(mapper.Map<IEnumerable<OrderReadDto>>(orderRepository.GetAllOrders()));
    }

    [HttpGet("{id}", Name = "GetOrderById")]
    public ActionResult<OrderReadDto> GetOrderById(int id)
    {
        return Ok(mapper.Map<OrderReadDto>(orderRepository.GetOrderById(id)));
    }

    [HttpPost("PlaceOrder")]
    public async Task<ActionResult<OrderReadDto>> PlaceOrder(OrderCreateDto createDto)
    {
        //create order
        var orderModel = mapper.Map<Order>(createDto);

        //initialize orderItems and calculate total
        var total = 0.0;
        var items = orderModel.OrderItems.ToList();
        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];

            //try to get data from PizzaService and fill it into OrderItems
            try
            {
                var pizza = await httpPizzaDataClient.GetPizzaById(item.PizzaId);

                if (pizza == null) return BadRequest();

                item.PizzaId = pizza.Id;

                if (!pizzaRepository.PizzaExistsByExternalId(pizza.Id))
                {
                    var pizzaModel = mapper.Map<Pizza>(pizza);
                    pizzaModel.Id = 0;
                    pizzaModel.ExternalId = pizza.Id;
                    pizzaRepository.CreatePizza(pizzaModel);
                    pizzaRepository.SaveChanges();
                }

                var sizeOption = pizza.SizeOptions.FirstOrDefault(opt => opt.Id == item.SizeOptionId);
                var borderOption = pizza.BorderOptions.FirstOrDefault(opt => opt.Id == item.BorderOptionId);

                if (sizeOption == null || borderOption == null) return BadRequest();

                item.SizeOption = sizeOption.Name;
                item.BorderOption = borderOption.Name;
                item.Price = pizza.BasePrice + sizeOption.PriceModifier + borderOption.PriceModifier;
                total += item.Price;
            }
            catch (Exception e)
            {
                Console.WriteLine($"---> Could Not Get Data From Pizza Service: {e}");
                return BadRequest();
            }
        }

        orderModel.OrderItems = items;
        orderModel.Total = total;
        orderRepository.CreateOrder(orderModel);
        if (!orderRepository.SaveChanges()) return BadRequest();

        return CreatedAtRoute(nameof(GetOrderById), new { orderModel.Id }, mapper.Map<OrderReadDto>(orderModel));
    }
}