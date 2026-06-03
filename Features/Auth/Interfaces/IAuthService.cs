using AuthService.Features.Auth.DTOs;

namespace AuthService.Features.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest loginRequest);
    Task<LoginResponse?> RegisterAsync(RegisterRequest registerRequest);
}