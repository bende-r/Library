using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;

using AutoMapper;

using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.AuthUseCases.Login;

public class LoginHandler: IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private ITokenService _jwtTokenGenerator;

    public LoginHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenService jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);
            
        if (user == null)
        {
            throw new LoginException(ExceptionMessages.LoginFailed);
        }

        var isValid = await _unitOfWork.Users.CheckPasswordAsync(user, request.Password);

        if (isValid == false)
        {
            throw new LoginException(ExceptionMessages.LoginFailed);
        }

        var roles = await _unitOfWork.Users.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);
            
        var refreshToken = _jwtTokenGenerator.CreateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenEndDate = DateTime.Now.AddDays(7).ToUniversalTime();

        await _unitOfWork.Users.UpdateAsync(user);
            
        await _unitOfWork.Save();
            
            
        var userDto = _mapper.Map<UserDto>(user);


        return new LoginResponse()
        {
            User = userDto,
            Token = token,
        };
    }
}