using Application.DTOs;

using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.AuthUseCases.Register;

public class RegisterMapper : Profile
{
    public RegisterMapper()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
        CreateMap<RegisterRequest, ApplicationUser>()
            .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedEmail, act => act.MapFrom(src => src.Email.ToUpper()));
    }
}