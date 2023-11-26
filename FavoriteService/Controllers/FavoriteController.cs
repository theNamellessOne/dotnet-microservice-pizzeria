using AutoMapper;
using FavoriteService.Data.Repositories;
using FavoriteService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteService.Controllers;

[Route("api/favorite/user/{userId}")]
[ApiController]
public class FavoriteController(
        IFavoriteRepository favoriteRepository,
        IPizzaRepository pizzaRepository,
        IUserRepository userRepository,
        IMapper mapper
    )
    : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PizzaReadDto>> GetUserFavorites(int userId)
    {
        Console.WriteLine(userId);
        var userModel = userRepository.GetUserById(userId);
        if (userModel == null) return NotFound();

        return Ok(
            mapper.Map<IEnumerable<PizzaReadDto>>(favoriteRepository.GetUserFavoritePizzas(userId))
        );
    }

    [HttpPost("{pizzaId}")]
    public ActionResult AddPizzaToUserFavorites(int userId, int pizzaId)
    {
        var userModel = userRepository.GetUserById(userId);
        var pizzaModel = pizzaRepository.GetPizzaById(pizzaId);

        //user or pizza with specified id does not exist
        if (userModel == null || pizzaModel == null) return NotFound();

        var favoriteModel = favoriteRepository.GetFavoriteByUserIdAndPizzaId(userId, pizzaId);

        //pizza already in user favorites
        if (favoriteModel != null) return BadRequest();

        favoriteRepository.AddToUserFavorites(userId, pizzaModel);
        favoriteRepository.SaveChanges();
        return Ok();
    }

    [HttpDelete("{pizzaId}")]
    public ActionResult RemovePizzaFromUserFavorites(int userId, int pizzaId)
    {
        var userModel = userRepository.GetUserById(userId);
        var pizzaModel = pizzaRepository.GetPizzaById(pizzaId);

        //user or pizza with specified id does not exist
        if (userModel == null || pizzaModel == null) return NotFound();

        var favoriteModel = favoriteRepository.GetFavoriteByUserIdAndPizzaId(userId, pizzaId);

        //pizza is not in user favorites
        if (favoriteModel == null) return BadRequest();

        favoriteRepository.RemoveFromUserFavorites(favoriteModel);
        favoriteRepository.SaveChanges();
        return Ok();
    }
}