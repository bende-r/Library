using FluentValidation;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN must be provided.")
                .Must(IsValidIsbn).WithMessage("Invalid ISBN format. It must be either ISBN-10 or ISBN-13.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title must be provided.");

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("Author must be provided.")
                .NotEqual(Guid.Empty).WithMessage("Author must be provided.");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre must be provided.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description must be provided.");
        }

        private bool IsValidIsbn(string isbn)
        {
            return IsbnValidator.IsValidIsbn(isbn);
        }
    }
}