using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new ApplicationException("Invalid username or password");
            }

            return _jwtTokenService.GenerateAccessToken(user);
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password)
            };

            await _userRepository.CreateAsync(user);
            return true;
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Extract the hash and the salt from the hashed password
            var parts = passwordHash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            // Hash the input password with the same salt
            using (var hmac = new HMACSHA256(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hash);
            }
        }

        private string HashPassword(string password)
        {
            // Generate a salt
            using (var hmac = new HMACSHA256())
            {
                var salt = hmac.Key;
                // Hash the password with the generated salt
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Return salt and hash as base64 strings
                return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
            }
        }
    }
}
