using Application.DTOs;
using Application.UseCases.BooksUseCases.AddBook;
using Application.UseCases.BooksUseCases;

using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookResponse>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));
            CreateMap<AddBookCommand, Book>();
            CreateMap<UpdateBookCommand, Book>();
        }
    }
}