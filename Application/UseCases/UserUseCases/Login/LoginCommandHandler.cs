using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.UserUseCases.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    Errors = new[] { "Invalid login attempt." }
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                var token = _tokenService.GenerateAccessToken(user);
                return new LoginResponse { IsSuccess = true, Token = token };
            }

            return new LoginResponse
            {
                IsSuccess = false,
                Errors = new[] { "Invalid login attempt." }
            };
        }
    }

}
