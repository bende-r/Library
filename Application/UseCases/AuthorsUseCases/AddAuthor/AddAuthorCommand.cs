﻿using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class AddAuthorCommand : IRequest<AddAuthorResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
    }
}