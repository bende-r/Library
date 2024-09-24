using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.UseCases.AuthorsUseCases.AddAuthor;
using Application.UseCases.AuthorsUseCases;
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
