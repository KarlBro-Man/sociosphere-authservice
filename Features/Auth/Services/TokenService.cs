using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthService.Data;
using AuthService.Features.Auth.Interfaces;
using AuthService.Features.Auth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    private readonly AppDbContext _context;
    public TokenService(AppDbContext context)
    {
        _context = context;
    }
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

    public async Task<RefreshToken> CreateRefreshToken(long userId)
    {
        var tokenString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refreshToken = new RefreshToken
        {
            Token = tokenString,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            UserId = userId
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
        
        return refreshToken;
    }

    public async Task<bool> CheckRefreshToken(string token)
    {
        return await _context.RefreshTokens.AnyAsync(t =>
        t.Token == token &&
        t.RevokedAt == null &&
        t.ExpiresAt > DateTime.UtcNow);
    }

    public async Task<string?> NewAccessToken(string token)
    {
        var refreshTokenIsValid = await CheckRefreshToken(token);
        if (refreshTokenIsValid)
        {
            var refreshToken = await _context.RefreshTokens.Include(t => t.User).FirstOrDefaultAsync(t => t.Token == token);
            var user = refreshToken?.User;
            if (user == null)
            {   
                return null;
            }
            var newAccessToken = CreateAccessToken(user);
            return newAccessToken;
        }
        else
        {
            return null;
        }
    }
}