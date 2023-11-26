using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Repositories;
using OrderService.Dtos;

namespace OrderService.Controllers;

[Route("api/order/[controller]")]
[ApiController]
public class PizzaController(
    IPizzaRepository pizzaRepository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<PizzaReadDto> GetAllPizzas()
    {
        return Ok(mapper.Map<IEnumerable<PizzaReadDto>>(pizzaRepository.GetAllPizzas()));
    }
}