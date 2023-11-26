using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaService.Data.Repositories.Interfaces;
using PizzaService.Dtos.PizzaSizeOption;

namespace PizzaService.Controllers;

[Route("api/pizza/sizeOptions")]
[ApiController]
public class PizzaSizeOptionsController(
    IPizzaSizeOptionsRepository pizzaSizeOptionsRepository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PizzaSizeOptionReadDto>> GetPizzas()
    {
        return Ok(mapper.Map<IEnumerable<PizzaSizeOptionReadDto>>(pizzaSizeOptionsRepository.GetAllPizzaSizeOptions()));
    }

    [HttpGet("{id}", Name = "GetPizzaSizeOptionById")]
    public ActionResult<PizzaSizeOptionReadDto> GetPizzaById(int id)
    {
        return Ok(mapper.Map<PizzaSizeOptionReadDto>(pizzaSizeOptionsRepository.GetPizzaSizeOptionById(id)));
    }
}