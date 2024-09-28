using Application.UseCases.AuthorsUseCases;
using Application.UseCases.AuthorsUseCases.AddAuthor;

using AutoMapper;

using Domain.Entities;

namespace Application.Mappings
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, AuthorResponse>();
            CreateMap<UpdateAuthorCommand, Author>();
        }
    }
}