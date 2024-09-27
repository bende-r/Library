using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.BooksUseCases.AddBook;
using Application.UseCases.BooksUseCases.GetBooksByAuthor;
using Application.UseCases.BooksUseCases.GetPagedBooks;
using Application.UseCases.BooksUseCases.UploadBookCover;
using Application.UseCases.EventUseCases.GetAllUserEvents;
using Application.UseCases.UsersBooksUseCases.BorrowBook;
using Application.UseCases.UsersBooksUseCases.ReturnBook;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //// Получить все книги
        //[HttpGet]
        //public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
        //{
        //    var query = new GetAllBooksQuery();
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return Ok(result);
        //}

        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooksAsync(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllBooksQuery(page, pageSize), cancellationToken);
            return Ok(result);
        }

        [HttpPost("getPagedBooks")]
        public async Task<IActionResult> GetPagedEvents([FromBody] GetPagedBooksRequest pageParams, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(pageParams, cancellationToken);
            var metadata = new
            {
                res.books.TotalCount,
                res.books.PageSize,
                res.books.CurrentPage,
                res.books.TotalPages,
                res.books.HasNext,
                res.books.HasPrevious,
              
            };

            HttpContext.Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(res.books);
        }

        // Получить книгу по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBookByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // Создать книгу
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetBookById), new { id = result.Id }, result);
        }

        // Обновить книгу
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        // Удалить книгу
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteBookCommand(id);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IActionResult> GetBookByIsbn(string isbn, CancellationToken cancellationToken)
        {
            var query = new GetBookByIsbnQuery(isbn);
            var book = await _mediator.Send(query, cancellationToken);
            return Ok(book);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(Guid authorId, CancellationToken cancellationToken)
        {
            var query = new GetBooksByAuthorQuery(authorId);
            var books = await _mediator.Send(query, cancellationToken);
            return Ok(books);
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok("Book borrowed successfully");
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok("Book returned successfully");
        }

        [HttpGet("getUserBooks/{id}")]
        public async Task<IActionResult> GetAllUserBooks(string id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUserBooksRequest(id), cancellationToken);

            return Ok(response.books);
        }

        [HttpPost("uploadPicture")]
        public async Task<object> UploadPicture([FromForm] IFormFile file, [FromForm] string bookId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new UploadBookCoverRequest(file, bookId), cancellationToken);
            return Ok(response.Message);
        }
    }
}