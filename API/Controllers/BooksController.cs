using Application.DTOs;
using Application.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<BookDto>> GetBookByISBN(string isbn)
        {
            var book = await _bookService.GetBookByISBNAsync(isbn);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDto>> CreateBook(BookDto bookDto)
        {
            var createdBook = await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }

            await _bookService.UpdateBookAsync(bookDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByAuthor(int authorId)
        {
            var books = await _bookService.GetBooksByAuthorIdAsync(authorId);
            return Ok(books);
        }
    }
}