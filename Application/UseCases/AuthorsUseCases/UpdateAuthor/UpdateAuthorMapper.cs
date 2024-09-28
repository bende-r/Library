using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class UpdateAuthorMapper : Profile
    {
        public UpdateAuthorMapper()
        {
            CreateMap<Author, UpdateAuthorResponse>();
            CreateMap<UpdateAuthorCommand, Author>();
        }
    }
}