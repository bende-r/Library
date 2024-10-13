using FluentValidation;

namespace Application.UseCases.BooksUseCases.GetPagedBooks
{
    public class GetPagedBooksRequestValidator : AbstractValidator<GetPagedBooksRequest>
    {
        public GetPagedBooksRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(GetPagedBooksRequest.MaxPageSize)
                .WithMessage($"Page size must be less than or equal to {GetPagedBooksRequest.MaxPageSize}.");
        }
    }
}