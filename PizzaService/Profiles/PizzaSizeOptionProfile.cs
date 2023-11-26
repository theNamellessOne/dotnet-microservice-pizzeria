using AutoMapper;
using PizzaService.Dtos.PizzaSizeOption;
using PizzaService.Models;

namespace PizzaService.Profiles;

//визначення того, як AutoMapper буде мапити класи
public class PizzaSizeOptionSizeOptionProfile : Profile
{
    public PizzaSizeOptionSizeOptionProfile()
    {
        //map pizzaSizeOption to pizzaSizeOptionReadDto
        CreateMap<PizzaSizeOption, PizzaSizeOptionReadDto>();

        //map pizzaSizeOptionCreateDto to pizzaSizeOption
        CreateMap<PizzaSizeOptionCreateDto, PizzaSizeOption>();
    }
}