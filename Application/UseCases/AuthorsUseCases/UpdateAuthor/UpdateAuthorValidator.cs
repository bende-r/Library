using FluentValidation;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(a => a.LastName).NotEmpty().MaximumLength(50);
            RuleFor(a => a.DateOfBirth).NotEmpty();
            RuleFor(a => a.Country).NotEmpty().MaximumLength(50);
        }
    }
}