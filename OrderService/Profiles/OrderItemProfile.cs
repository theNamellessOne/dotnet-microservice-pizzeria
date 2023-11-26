using AutoMapper;
using OrderService.Dtos;
using OrderService.Models;

namespace OrderService.Profiles;

//defines how AutoMapper will map objects
public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        //map orderItem to orderItemReadDto
        CreateMap<OrderItem, OrderItemReadDto>();

        //map orderItemCreateDto to orderItem
        CreateMap<OrderItemCreateDto, OrderItem>();
    }
}