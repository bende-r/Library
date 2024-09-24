using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class AddAuthorValidator : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(a => a.LastName).NotEmpty().MaximumLength(50);
            RuleFor(a => a.DateOfBirth).NotEmpty();
            RuleFor(a => a.Country).NotEmpty().MaximumLength(50);
        }
    }
}
