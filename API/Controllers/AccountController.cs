using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.UserUseCases.AssignRole;
using Application.UseCases.UserUseCases.CreateRole;
using Application.UseCases.UserUseCases.RefreshToken;
using Application.UseCases.UserUseCases.Registration;

using Domain.Entities;

using Infrastructure.Data;

using MediatR;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(model, cancellationToken);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(model, cancellationToken);
            return Ok(response);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRoleAsync([FromBody] AssignRoleRequest model, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] string roleName)
        {
            var response = await _mediator.Send(new CreateRoleRequest(roleName));

            return Ok(response);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshToken, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(refreshToken, cancellationToken);

            return Ok(response);
        }
    }

   
}
