using AutoMapper;
using FavoriteService.Dtos;
using FavoriteService.Models;
using UserService;

namespace FavoriteService.Profiles;

//defines how AutoMapper will map objects
public class UserProfile : Profile
{
    public UserProfile()
    {
        //map user to userReadDto
        //user.ExternalId is mapped to userReadDto.Id
        CreateMap<User, UserReadDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ExternalId));

        //map userPublishDto to user
        //userPublishDto.Id is mapped to user.ExternalId
        CreateMap<UserPublishDto, User>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));


        //map grpcUserModel to user (generated)
        CreateMap<GrpcUserModel, User>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));
    }
}