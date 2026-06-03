using AuthService.Data;
using AuthService.Features.Auth.DTOs;
using AuthService.Features.Auth.Interfaces;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Features.Auth.Services;

public class AuthService : IAuthService{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    public AuthService(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
    {
        try
        {
            var email = loginRequest.Username;
            var password = loginRequest.Password;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if(!isValid)
                return null;

            var accessToken = _tokenService.CreateAccessToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();

            var response = new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return response;
        }
        catch(Exception e)
        {
            throw new Exception("Error in login", e);
        }
    }
}