using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaService.Data.Repositories.Interfaces;
using PizzaService.Dtos.PizzaBorderOption;

namespace PizzaService.Controllers;

[Route("api/pizza/borderOptions")]
[ApiController]
public class PizzaBorderOptionsController(
    IPizzaBorderOptionsRepository pizzaBorderOptionsRepository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PizzaBorderOptionReadDto>> GetPizzas()
    {
        return Ok(mapper.Map<IEnumerable<PizzaBorderOptionReadDto>>(pizzaBorderOptionsRepository
            .GetAllPizzaBorderOptions()));
    }

    [HttpGet("{id}", Name = "GetPizzaBorderOptionById")]
    public ActionResult<PizzaBorderOptionReadDto> GetPizzaById(int id)
    {
        return Ok(mapper.Map<PizzaBorderOptionReadDto>(pizzaBorderOptionsRepository.GetPizzaBorderOptionById(id)));
    }
}