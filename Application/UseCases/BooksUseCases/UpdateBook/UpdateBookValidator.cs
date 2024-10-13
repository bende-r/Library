using FluentValidation;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Must(IsValidIsbn).WithMessage("Invalid ISBN format. It must be either ISBN-10 or ISBN-13.");

            RuleFor(b => b.Title).NotEmpty().Length(2, 50);
            RuleFor(b => b.Genre).NotEmpty().Length(2, 30);
            RuleFor(b => b.Description).NotEmpty().Length(2, 300);
            RuleFor(b => b.AuthorId).NotEmpty();
        }

        private bool IsValidIsbn(string isbn)
        {
            return IsbnValidator.IsValidIsbn(isbn);
        }
    }
}