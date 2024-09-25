using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.UseCases.AuthorsUseCases.AddAuthor;
using Application.UseCases.AuthorsUseCases;
using Domain.Entities;
using AutoMapper;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class AddBookMapper: Profile
    {
        public AddBookMapper() {

            CreateMap<Book, AddBookResponse>();
            CreateMap<AddBookCommand, Book>();
        }
    }
}
