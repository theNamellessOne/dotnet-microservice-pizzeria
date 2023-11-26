using AutoMapper;
using PizzaService.Dtos.Pizza;
using PizzaService.Models;

namespace PizzaService.Profiles;

//визначення того, як AutoMapper буде мапити класи
public class PizzaProfile : Profile
{
    public PizzaProfile()
    {
        //map pizza to pizzaReadDto
        CreateMap<Pizza, PizzaReadDto>();

        //map pizzaReadDto to pizzaPublishDto
        CreateMap<PizzaReadDto, PizzaPublishDto>();

        //map pizzaCreateDto to pizza
        CreateMap<PizzaCreateDto, Pizza>();

        //map pizza to grpcPizzaModel(generated)
        CreateMap<Pizza, GrpcPizzaModel>();
    }
}