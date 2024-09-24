using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.BooksUseCases.AddBook;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Получить все книги
        [HttpGet]
        public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
        {
            var query = new GetAllBooksQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
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
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetBookById), new { id = result.Id }, result);
        }

        // Обновить книгу
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        // Удалить книгу
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteBookCommand(id);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}