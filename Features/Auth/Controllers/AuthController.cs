using AuthService.Features.Auth.DTOs;
using AuthService.Features.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Features.Auth.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var accessToken = "accessToken";
        var refreshToken = "refreshToken";

        var response = new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        return Ok(response);
    }
}