using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class AddBookMapper : Profile
    {
        public AddBookMapper()
        {
            CreateMap<Book, AddBookResponse>();
            CreateMap<AddBookCommand, Book>();
        }
    }
}