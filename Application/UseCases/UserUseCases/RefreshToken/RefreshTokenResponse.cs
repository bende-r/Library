using Application.DTOs;

namespace Application.UseCases.AuthUseCases.RefreshToken;

public class RefreshTokenResponse
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}