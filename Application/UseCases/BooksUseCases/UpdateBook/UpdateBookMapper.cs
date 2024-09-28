using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class UpdateBookMapper : Profile
    {
        public UpdateBookMapper()
        {
            CreateMap<Book, UpdateBookResponse>();
            CreateMap<UpdateBookCommand, Book>();
        }
    }
}