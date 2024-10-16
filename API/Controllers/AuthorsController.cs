﻿using Application.UseCases.AuthorsUseCases.AddAuthor;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Получить всех авторов
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken)
        {
            var query = new GetAllAuthorsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        // Получить автора по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetAuthorByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        // Создать автора
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetAuthorById), new { id = result.Id }, result);
        }

        // Обновить автора
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        // Удалить автора
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteAuthorCommand(id);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}