using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.EventUseCases.GetAllUserEvents;

public class GetAllUserBooksMapper : Profile
{
    public GetAllUserBooksMapper()
    {
        CreateMap<Book, GetAllUserBooksResponse>();
    }
}