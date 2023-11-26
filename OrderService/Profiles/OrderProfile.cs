using AutoMapper;
using OrderService.Dtos;
using OrderService.Models;

namespace OrderService.Profiles;

//defines how AutoMapper will map objects
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        //map order to orderReadDto
        CreateMap<Order, OrderReadDto>();

        //map orderCreateDto to order
        CreateMap<OrderCreateDto, Order>();
    }
}