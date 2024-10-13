using Domain.Entities;

namespace Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);

    public string CreateRefreshToken();
}