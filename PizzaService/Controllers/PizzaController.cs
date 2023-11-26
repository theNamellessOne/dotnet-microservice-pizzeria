using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaService.AsyncDataServices;
using PizzaService.Data.Repositories.Interfaces;
using PizzaService.Dtos.Pizza;
using PizzaService.Models;

namespace PizzaService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PizzaController(
    IMessageBusClient messageBusClient,
    IPizzaRepository pizzaRepository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PizzaReadDto>> GetPizzas()
    {
        return Ok(mapper.Map<IEnumerable<PizzaReadDto>>(pizzaRepository.GetAllPizzas()));
    }

    [HttpGet("{id}", Name = "GetPizzaById")]
    public ActionResult<PizzaReadDto> GetPizzaById(int id)
    {
        return Ok(mapper.Map<PizzaReadDto>(pizzaRepository.GetPizzaById(id)));
    }

    [HttpPost]
    public ActionResult<PizzaReadDto> CreatePizza(PizzaCreateDto pizzaCreateDto)
    {
        var pizzaModel = mapper.Map<Pizza>(pizzaCreateDto);
        pizzaRepository.CreatePizza(pizzaModel);
        pizzaRepository.SaveChanges();

        var pizzaReadDto = mapper.Map<PizzaReadDto>(pizzaModel);

        //публікування нового юзера на шину повідомленнь, щоб інші сервіси могли відреагувати
        var pizzaPublishDto = mapper.Map<PizzaPublishDto>(pizzaReadDto);
        pizzaPublishDto.Event = "Pizza_Created";
        messageBusClient.PublishPizza(pizzaPublishDto);

        return CreatedAtRoute(nameof(GetPizzaById), new { pizzaReadDto.Id }, pizzaReadDto);
    }

    [HttpDelete("{id}")]
    public ActionResult RemovePizza(int id)
    {
        var pizzaModel = pizzaRepository.GetPizzaById(id);

        if (pizzaModel == null) return BadRequest();

        pizzaRepository.RemovePizza(pizzaModel);
        pizzaRepository.SaveChanges();

        //публікування повідмлення про видалення юзера, щоб інші сервіси могли відреагувати
        var pizzaPublishDto = mapper.Map<PizzaPublishDto>(mapper.Map<PizzaReadDto>(pizzaModel));
        pizzaPublishDto.Event = "Pizza_Removed";
        messageBusClient.PublishPizza(pizzaPublishDto);

        return Ok(pizzaModel.Id);
    }
}