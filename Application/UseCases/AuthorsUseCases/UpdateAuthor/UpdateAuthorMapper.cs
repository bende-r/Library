using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Domain.Entities;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class UpdateAuthorMapper: Profile
    {
        public UpdateAuthorMapper()
        {
            CreateMap<Author, UpdateAuthorResponse>();
            CreateMap<UpdateAuthorCommand, Author>();
        }
    }
}
