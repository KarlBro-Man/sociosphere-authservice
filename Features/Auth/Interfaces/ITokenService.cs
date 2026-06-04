using AuthService.Features.Auth.Models;

namespace AuthService.Features.Auth.Interfaces;

public interface ITokenService
{
    string CreateAccessToken(User user);
    Task<RefreshToken> CreateRefreshToken(long userId);
    Task<string?> NewAccessToken(string token);
}