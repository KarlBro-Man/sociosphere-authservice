using AuthService.Features.Auth.Models;

namespace AuthService.Features.Auth.Interfaces;

public interface ITokenService
{
    string CreateAccessToken(User user);
    string CreateRefreshToken();
}