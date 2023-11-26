using AutoMapper;
using FavoriteService.Data.Repositories;
using FavoriteService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteService.Controllers;

[Route("api/favorite/[controller]")]
[ApiController]
public class UserController(
        IUserRepository userRepository,
        IMapper mapper
    )
    : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<UserReadDto>> GetUsers()
    {
        return Ok(
            mapper.Map<IEnumerable<UserReadDto>>(userRepository.GetAllUsers())
        );
    }
}