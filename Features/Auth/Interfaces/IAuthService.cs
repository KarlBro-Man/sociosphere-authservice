using AuthService.Features.Auth.DTOs;

namespace AuthService.Features.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginRegisterResultDto?> LoginAsync(LoginRequest loginRequest);
    Task<LoginRegisterResultDto?> RegisterAsync(RegisterRequest registerRequest);
}