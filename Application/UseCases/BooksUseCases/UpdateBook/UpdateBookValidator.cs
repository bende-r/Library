using FluentValidation;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(b => b.ISBN).NotEmpty().Length(10, 13);
            RuleFor(b => b.Title).NotEmpty().Length(2, 50);
            RuleFor(b => b.Genre).NotEmpty().Length(2, 30);
            RuleFor(b => b.Description).NotEmpty().Length(2, 300);
            RuleFor(b => b.AuthorId).NotEmpty();
        }
    }
}