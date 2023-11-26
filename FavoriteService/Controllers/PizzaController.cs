using AutoMapper;
using FavoriteService.Data.Repositories;
using FavoriteService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteService.Controllers;

[Route("api/favorite/[controller]")]
[ApiController]
public class PizzaController(
        IPizzaRepository pizzaRepository,
        IMapper mapper
    )
    : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PizzaReadDto>> GetPizzas()
    {
        return Ok(
            mapper.Map<IEnumerable<PizzaReadDto>>(pizzaRepository.GetAllPizzas())
        );
    }
}