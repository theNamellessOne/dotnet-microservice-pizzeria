using AutoMapper;
using FavoriteService.Dtos;
using FavoriteService.Models;
using PizzaService;

namespace FavoriteService.Profiles;

//defines how AutoMapper will map objects
public class PizzaProfile : Profile
{
    public PizzaProfile()
    {
        //map pizza to pizzaReadDto
        //pizza.ExternalId is mapped to pizzaReadDto.id
        CreateMap<Pizza, PizzaReadDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ExternalId));

        //map pizzaPublishDto to pizza
        //pizzaPublishDto.Id is mapped to pizza.ExternalId
        CreateMap<PizzaPublishDto, Pizza>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));

        //map grpcCoffeeModel to pizza (generated)
        CreateMap<GrpcPizzaModel, Pizza>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));
    }
}