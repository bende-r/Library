using Application.DTOs;
using Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            var createdAuthor = await _authorService.AddAuthorAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
        {
            if (id != authorDto.Id)
            {
                return BadRequest();
            }

            await _authorService.UpdateAuthorAsync(authorDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}