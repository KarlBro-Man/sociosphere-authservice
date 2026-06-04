using AuthService.Features.Auth.Models;

namespace AuthService.Features.Auth.DTOs;
public class LoginRegisterResultDto
{
    public string AccessToken {get; set;}
    public RefreshToken RefreshToken {get; set;}
}