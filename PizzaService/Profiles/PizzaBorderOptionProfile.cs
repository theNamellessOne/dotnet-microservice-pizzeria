using AutoMapper;
using PizzaService.Dtos.PizzaBorderOption;
using PizzaService.Models;

namespace PizzaService.Profiles;

//визначення того, як AutoMapper буде мапити класи
public class PizzaBorderOptionBorderOptionProfile : Profile
{
    public PizzaBorderOptionBorderOptionProfile()
    {
        //map pizzaBorderOption to pizzaBorderOptionReadDto
        CreateMap<PizzaBorderOption, PizzaBorderOptionReadDto>();

        //map pizzaBorderOptionCreateDto to pizzaBorderOption
        CreateMap<PizzaBorderOptionCreateDto, PizzaBorderOption>();
    }
}