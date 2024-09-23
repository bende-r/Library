using Application.DTOs;
using Application.Interfaces;

using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<PagedResult<BookDto>> GetAllBooksAsync(int page, int pageSize)
        {
            var books = await _unitOfWork.Books.GetAllPaginatedAsync(page, pageSize);
            var totalBooks = await _unitOfWork.Books.GetTotalCountAsync();

            return new PagedResult<BookDto>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalBooks,
                TotalPages = (int)Math.Ceiling((double)totalBooks / pageSize),
                Items = _mapper.Map<IEnumerable<BookDto>>(books)
            };
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> AddBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync(); // Сохранение через UnitOfWork
            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.SaveChangesAsync(); // Сохранение через UnitOfWork
        }

        public async Task DeleteBookAsync(int id)
        {
            await _unitOfWork.Books.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync(); // Сохранение через UnitOfWork
        }

        public async Task<IEnumerable<BookDto>> GetBooksByAuthorIdAsync(int authorId)
        {
            var books = await _unitOfWork.Books.GetByAuthorIdAsync(authorId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByISBNAsync(string isbn)
        {
           var books = await _unitOfWork.Books.GetByISBNAsync(isbn);
            return _mapper.Map<BookDto>(books);
        }
    }
}
