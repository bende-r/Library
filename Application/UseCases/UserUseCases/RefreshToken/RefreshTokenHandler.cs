using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;

using AutoMapper;

using Domain.Interfaces;
using Domain.Models.Entities;

using FluentValidation;
using MediatR;

namespace Application.UseCases.AuthUseCases.RefreshToken;

public class RefreshTokenHandler: IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private ITokenService _jwtTokenGenerator;

    public RefreshTokenHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenService jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByRefreshTokenAsync(request.refreshToken, cancellationToken);
            
        if (user == null)
        {
            throw new LoginException(ExceptionMessages.LoginFailed);
        }
            
        if(user.RefreshTokenEndDate < DateTime.Now)
        {
            throw new LoginException("Refresh token expired");
        }

        var roles = await _unitOfWork.Users.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);
            
        var refreshToken = _jwtTokenGenerator.CreateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenEndDate = DateTime.Now.AddDays(7).ToUniversalTime();

        await _unitOfWork.Users.UpdateAsync(user);
            
        await _unitOfWork.Save();
            
        var userDto = _mapper.Map<UserDto>(user);
        
        return new RefreshTokenResponse()
        {
            User = userDto,
            Token = token,
        };
    }
}