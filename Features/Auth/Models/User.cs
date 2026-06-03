using System.ComponentModel.DataAnnotations;

namespace AuthService.Features.Auth.Models;

public class User
{
    public long Id {get; set;}
    public string Email {get; set;}
    public string PasswordHash {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public bool IsActive {get; set;}

    public List<RefreshToken> RefreshTokens {get; set;} = new();
}