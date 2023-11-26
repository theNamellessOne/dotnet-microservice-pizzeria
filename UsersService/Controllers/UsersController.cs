using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.AsyncDataServices;
using UserService.Data.Repositories;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    IMessageBusClient messageBusClient,
    IUsersRepository usersRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
    {
        return Ok(mapper.Map<IEnumerable<UserReadDto>>(usersRepository.GetAllUsers()));
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public ActionResult<UserReadDto> GetUserById(int id)
    {
        return Ok(mapper.Map<UserReadDto>(usersRepository.GetUserById(id)));
    }

    [HttpPost]
    public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
    {
        var userModel = mapper.Map<User>(userCreateDto);
        usersRepository.CreateUser(userModel);
        usersRepository.SaveChanges();

        var userReadDto = mapper.Map<UserReadDto>(userModel);

        //публікування нового юзера на шину повідомленнь, щоб інші сервіси могли відреагувати
        var userPublishDto = mapper.Map<UserPublishDto>(userReadDto);
        userPublishDto.Event = "User_Created";
        messageBusClient.PublishUser(userPublishDto);

        return CreatedAtRoute(nameof(GetUserById), new { userReadDto.Id }, userReadDto);
    }
}