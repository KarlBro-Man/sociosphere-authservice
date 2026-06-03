using AuthService.Features.Auth.DTOs;
using AuthService.Features.Auth.Interfaces;
using AuthService.Features.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Features.Auth.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _authService.LoginAsync(loginRequest);

        if(result == null)
        {
            return BadRequest("Invalid Credentials");
        }

        return result;
    }
}