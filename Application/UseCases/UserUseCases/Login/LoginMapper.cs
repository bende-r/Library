using Application.DTOs;

using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.AuthUseCases.Login;

public class LoginMapper : Profile
{
    public LoginMapper()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
}