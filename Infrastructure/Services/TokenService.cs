using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Application.Interfaces;

using Domain.Entities;
using Domain.Models.Entities;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JwtToken _jwtOptions;

    public TokenService(IOptions<JwtToken> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var claimList = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
            new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
        };

        claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer,
            Subject = new ClaimsIdentity(claimList),
            IssuedAt = DateTime.UtcNow,
            Expires = _jwtOptions.Expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
        {
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}

//private readonly UserManager<ApplicationUser> _userManager;
//private readonly IConfiguration _configuration;

//public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
//{
//    _userManager = userManager;
//    _configuration = configuration;
//}

//public string GenerateAccessToken(ApplicationUser user)
//{
//    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

//    var claims = new List<Claim>
//    {
//        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//        new Claim(ClaimTypes.Name, user.UserName)
//    };

//    var roles = _userManager.GetRolesAsync(user).Result;
//    foreach (var role in roles)
//    {
//        claims.Add(new Claim(ClaimTypes.Role, role));
//    }

//    var tokenDescriptor = new SecurityTokenDescriptor
//    {
//        Subject = new ClaimsIdentity(claims),
//        Expires = DateTime.UtcNow.AddMinutes(15),
//        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
//    };

//    var tokenHandler = new JwtSecurityTokenHandler();
//    var token = tokenHandler.CreateToken(tokenDescriptor);

//    return tokenHandler.WriteToken(token);
//}

//public string GenerateRefreshToken()
//{
//    var randomNumber = new byte[32];
//    using (var rng = RandomNumberGenerator.Create())
//    {
//        rng.GetBytes(randomNumber);
//        return Convert.ToBase64String(randomNumber);
//    }
//}

//public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
//{
//    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
//    var tokenHandler = new JwtSecurityTokenHandler();

//    var tokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = false
//    };

//    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
//    var jwtSecurityToken = securityToken as JwtSecurityToken;
//    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
//    {
//        throw new SecurityTokenException("Invalid token");
//    }

//    return principal;
//}

//public async Task<RefreshTokenResponse> RefreshTokenAsync(string token, string refreshToken)
//{
//    var principal = GetPrincipalFromExpiredToken(token);
//    var userId = principal.Identity.Name;

//    var user = await _userManager.FindByIdAsync(userId);
//    if (user == null || user.RefreshToken != refreshToken)
//    {
//        throw new SecurityTokenException("Invalid refresh token");
//    }

//    var newAccessToken = GenerateAccessToken(user);
//    var newRefreshToken = GenerateRefreshToken();

//    // Сохранение нового refresh токена в базе данных
//    user.RefreshToken = newRefreshToken;
//    await _userManager.UpdateAsync(user);

//    return new RefreshTokenResponse
//    {
//        AccessToken = newAccessToken,
//        RefreshToken = newRefreshToken
//    };
//}