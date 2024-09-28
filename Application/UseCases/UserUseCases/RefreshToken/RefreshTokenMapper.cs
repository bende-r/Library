using Application.DTOs;

using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.EventUseCases.DeleteEvent;

public class RefreshTokenMapper : Profile
{
    public RefreshTokenMapper()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
}