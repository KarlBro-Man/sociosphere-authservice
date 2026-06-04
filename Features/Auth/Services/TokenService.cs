using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthService.Features.Auth.Interfaces;
using AuthService.Features.Auth.Models;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    public string CreateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            // new Claim(ClaimTypes.NameIdentifier, user.Email),
            // new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim (JwtRegisteredClaimNames.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("THIS_IS_A_LONG_RANDOM_SECRET_KEY_AT_LEAST_32_CHARS")
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "jwt:issuer",
            audience: "jwt:audience",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string CreateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}