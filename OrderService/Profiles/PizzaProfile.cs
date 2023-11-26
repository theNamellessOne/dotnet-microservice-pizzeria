using AutoMapper;
using OrderService.Dtos;
using OrderService.Dtos.HttpGetDtos;
using OrderService.Models;
using PizzaService;

namespace OrderService.Profiles;

//defines how AutoMapper will map objects
public class PizzaProfile : Profile
{
    public PizzaProfile()
    {
        //map pizzaPublishDto to pizza
        CreateMap<PizzaPublishDto, Pizza>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));


        //map pizza to pizzaPublishDto
        CreateMap<Pizza, PizzaReadDto>();

        //map pizzaHttpGetDto to pizza
        CreateMap<PizzaHttpGetDto, Pizza>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));

        //map grpcPizzaModel to pizza (generated)
        CreateMap<GrpcPizzaModel, Pizza>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));
    }
}