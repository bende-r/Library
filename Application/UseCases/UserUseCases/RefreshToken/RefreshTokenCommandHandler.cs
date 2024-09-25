using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.UserUseCases.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _tokenService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (result.IsSuccess)
            {
                return new RefreshTokenResponse
                {
                    IsSuccess = true,
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                };
            }

            return new RefreshTokenResponse
            {
                IsSuccess = false,
                Errors = result.Errors
            };
        }
    }

}
