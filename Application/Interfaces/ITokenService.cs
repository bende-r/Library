

using Domain.Entities;
using System.Security.Claims;

namespace Application.Interfaces;


    public interface ITokenService
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    public string CreateRefreshToken();
    //string GenerateAccessToken(ApplicationUser user);
    //string GenerateRefreshToken();
    //ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    //Task<RefreshTokenResponse> RefreshTokenAsync(string token, string refreshToken);
}


