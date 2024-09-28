using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class AddAuthorMapper : Profile
    {
        public AddAuthorMapper()
        {
            CreateMap<Author, AddAuthorResponse>();
            CreateMap<AddAuthorCommand, Author>();
        }
    }
}