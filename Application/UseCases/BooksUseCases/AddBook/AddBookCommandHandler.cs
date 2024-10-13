using Application.Exceptions;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using FluentValidation;

using MediatR;

namespace Application.UseCases.BooksUseCases.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, AddBookResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddBookCommand> _validator;

        public AddBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AddBookCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<AddBookResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            // Применение валидации через FluentValidation
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
            }

            // Проверка на дублирование книги с таким же ISBN
            var existingBook = await _unitOfWork.Books.FindAsync(b => b.ISBN == request.ISBN);
            if (existingBook != null)
            {
                throw new AlreadyExistsException("A book with the same ISBN already exists.");
            }

            // Создание новой книги
            var book = _mapper.Map<Book>(request);
            book.IsBorrowed = false; // Новая книга не взята по умолчанию

            // Добавление книги
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AddBookResponse>(book);
        }
    }
}