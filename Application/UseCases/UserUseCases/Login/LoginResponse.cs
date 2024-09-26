using Application.DTOs;

namespace Application.UseCases.AuthUseCases.Login;

public class LoginResponse
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}