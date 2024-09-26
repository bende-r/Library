using Application.DTOs;

using AutoMapper;

using Domain.Entities;
using Domain.Models.Entities;

namespace Application.UseCases.EventUseCases.DeleteEvent;

public class RefreshTokenMapper: Profile
{
    public RefreshTokenMapper()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
}